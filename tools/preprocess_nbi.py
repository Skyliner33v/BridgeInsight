#!/usr/bin/env python3
"""
NBI Data Preprocessor for BridgeInsight
Converts FHWA NBI delimited data (WA state) into JSON for Blazor WASM seeding.
"""

import csv
import json
import sys
import os

# WA County FIPS codes
WA_COUNTIES = {
    "001": "Adams", "003": "Asotin", "005": "Benton", "007": "Chelan",
    "009": "Clallam", "011": "Clark", "013": "Columbia", "015": "Cowlitz",
    "017": "Douglas", "019": "Ferry", "021": "Franklin", "023": "Garfield",
    "025": "Grant", "027": "Grays Harbor", "029": "Island", "031": "Jefferson",
    "033": "King", "035": "Kitsap", "037": "Kittitas", "039": "Klickitat",
    "041": "Lewis", "043": "Lincoln", "045": "Mason", "047": "Okanogan",
    "049": "Pacific", "051": "Pend Oreille", "053": "Pierce", "055": "San Juan",
    "057": "Skagit", "059": "Skamania", "061": "Snohomish", "063": "Spokane",
    "065": "Stevens", "067": "Thurston", "069": "Wahkiakum", "071": "Walla Walla",
    "073": "Whatcom", "075": "Whitman", "077": "Yakima"
}


def parse_lat_lon(lat_str, lon_str):
    """Convert NBI lat/lon format (DDMMSSFF) to decimal degrees."""
    try:
        lat = lat_str.strip()
        lon = lon_str.strip()
        if not lat or not lon:
            return None, None

        lat_val = int(lat)
        lon_val = int(lon)

        # Latitude: DDMMSSFF
        lat_deg = lat_val // 1000000
        lat_min = (lat_val % 1000000) // 10000
        lat_sec = (lat_val % 10000) / 100.0
        lat_decimal = lat_deg + lat_min / 60.0 + lat_sec / 3600.0

        # Longitude: DDDMMSSFF (negative for Western hemisphere)
        lon_deg = lon_val // 1000000
        lon_min = (lon_val % 1000000) // 10000
        lon_sec = (lon_val % 10000) / 100.0
        lon_decimal = -(lon_deg + lon_min / 60.0 + lon_sec / 3600.0)

        return round(lat_decimal, 6), round(lon_decimal, 6)
    except (ValueError, TypeError):
        return None, None


def safe_int(val):
    """Convert to int or None."""
    try:
        v = val.strip().strip("'")
        if v == '' or v == 'N' or v == ' ':
            return None
        return int(v)
    except (ValueError, TypeError):
        return None


def safe_float(val):
    """Convert to float or None."""
    try:
        v = val.strip().strip("'")
        if v == '' or v == ' ':
            return None
        return round(float(v), 1)
    except (ValueError, TypeError):
        return None


def safe_str(val):
    """Clean string value."""
    if val is None:
        return ""
    return val.strip().strip("'").strip()


def parse_inspection_date(date_str):
    """Parse NBI inspection date (MMYY format) to ISO date string."""
    try:
        d = date_str.strip().strip("'")
        if not d or len(d) < 4:
            return None
        month = int(d[:2])
        year = int(d[2:])
        if year < 100:
            year += 2000 if year < 50 else 1900
        if 1 <= month <= 12:
            return f"{year:04d}-{month:02d}-01"
        return None
    except (ValueError, TypeError):
        return None


def process_nbi_file(input_path, output_path):
    """Process NBI delimited file into JSON."""
    bridges = []

    with open(input_path, 'r', encoding='utf-8', errors='replace') as f:
        reader = csv.DictReader(f)

        for i, row in enumerate(reader):
            lat, lon = parse_lat_lon(
                row.get('LAT_016', ''),
                row.get('LONG_017', '')
            )

            county_code = safe_str(row.get('COUNTY_CODE_003', '')).zfill(3)

            bridge = {
                "id": i + 1,
                "stateCode": safe_str(row.get('STATE_CODE_001', '')),
                "structureNumber": safe_str(row.get('STRUCTURE_NUMBER_008', '')),
                "featuresIntersected": safe_str(row.get('FEATURES_DESC_006A', '')),
                "facilityCarried": safe_str(row.get('FACILITY_CARRIED_007', '')),
                "countyCode": county_code,
                "countyName": WA_COUNTIES.get(county_code, f"Unknown ({county_code})"),
                "latitude": lat or 0.0,
                "longitude": lon or 0.0,
                "mainSpanMaterial": safe_str(row.get('STRUCTURE_KIND_043A', '')),
                "mainSpanDesign": safe_str(row.get('STRUCTURE_TYPE_043B', '')),
                "serviceOnBridge": safe_str(row.get('SERVICE_ON_042A', '')),
                "serviceUnderBridge": safe_str(row.get('SERVICE_UND_042B', '')),
                "yearBuilt": safe_int(row.get('YEAR_BUILT_027', '')),
                "yearReconstructed": safe_int(row.get('YEAR_RECONSTRUCTED_106', '')),
                "deckCondition": safe_int(row.get('DECK_COND_058', '')),
                "superstructureCondition": safe_int(row.get('SUPERSTRUCTURE_COND_059', '')),
                "substructureCondition": safe_int(row.get('SUBSTRUCTURE_COND_060', '')),
                "culvertCondition": safe_int(row.get('CULVERT_COND_062', '')),
                "channelCondition": safe_int(row.get('CHANNEL_COND_061', '')),
                "waterwayAdequacy": safe_int(row.get('WATERWAY_EVAL_071', '')),
                "averageDailyTraffic": safe_int(row.get('ADT_029', '')),
                "truckTrafficPercent": safe_int(row.get('PERCENT_ADT_TRUCK_109', '')),
                "structureLength": safe_float(row.get('STRUCTURE_LEN_MT_049', '')),
                "bridgeRoadwayWidth": safe_float(row.get('ROADWAY_WIDTH_MT_051', '')),
                "approachRoadwayWidth": safe_float(row.get('APPR_WIDTH_MT_032', '')),
                "structuralEvaluation": safe_str(row.get('STRUCTURAL_EVAL_067', '')),
                "deckGeometryEvaluation": safe_str(row.get('DECK_GEOMETRY_EVAL_068', '')),
                "underclearanceEvaluation": safe_str(row.get('UNDCLRENCE_EVAL_069', '')),
                "approachRoadwayAlignment": safe_str(row.get('APPR_ROAD_EVAL_072', '')),
                "openPostedClosed": safe_str(row.get('OPEN_CLOSED_POSTED_041', '')),
                "nbisBridgeLength": safe_str(row.get('BRIDGE_LEN_IND_112', '')),
                "scourCritical": safe_str(row.get('SCOUR_CRITICAL_113', '')),
                "bridgePosting": safe_int(row.get('POSTING_EVAL_070', '')),
                "owner": safe_str(row.get('OWNER_022', '')),
                "maintenanceResponsibility": safe_str(row.get('MAINTENANCE_021', '')),
                "inspectionDate": parse_inspection_date(row.get('DATE_OF_INSPECT_090', '')),
                "inspectionFrequency": safe_int(row.get('INSPECT_FREQ_MONTHS_091', ''))
            }

            # Remove null values and empty strings to reduce file size
            bridge = {k: v for k, v in bridge.items() if v is not None and v != ""}
            # Always keep id, structureNumber, countyCode
            bridge.setdefault("id", i + 1)
            bridge.setdefault("structureNumber", "")
            bridge.setdefault("countyCode", "")
            bridges.append(bridge)

    # Write JSON output
    with open(output_path, 'w', encoding='utf-8') as f:
        json.dump(bridges, f, separators=(',', ':'))

    print(f"Processed {len(bridges)} bridges")
    print(f"Output: {output_path}")
    print(f"File size: {os.path.getsize(output_path) / 1024:.0f} KB")

    # Print some stats
    sd_count = sum(1 for b in bridges if any(
        b.get(f) is not None and b.get(f) <= 4
        for f in ['deckCondition', 'superstructureCondition', 'substructureCondition', 'culvertCondition']
    ))
    print(f"Structurally deficient: {sd_count}")

    counties = {}
    for b in bridges:
        cn = b['countyName']
        counties[cn] = counties.get(cn, 0) + 1
    top5 = sorted(counties.items(), key=lambda x: -x[1])[:5]
    print(f"Top 5 counties: {', '.join(f'{c}: {n}' for c, n in top5)}")


if __name__ == '__main__':
    script_dir = os.path.dirname(os.path.abspath(__file__))
    input_file = os.path.join(script_dir, 'raw', 'WA24.txt')
    output_file = os.path.join(script_dir, '..', 'src', 'BridgeInsight', 'wwwroot', 'data', 'wa-bridges-2024.json')

    if not os.path.exists(input_file):
        print(f"Error: Input file not found: {input_file}")
        print("Download it first:")
        print("  curl -L -o tools/raw/WA24.txt https://www.fhwa.dot.gov/bridge/nbi/2024/delimited/WA24.txt")
        sys.exit(1)

    process_nbi_file(input_file, output_file)
