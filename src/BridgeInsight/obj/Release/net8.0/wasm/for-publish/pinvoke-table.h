// GENERATED FILE, DO NOT MODIFY

void GlobalizationNative_ChangeCase (int,int,int,int,int);
void GlobalizationNative_ChangeCaseInvariant (int,int,int,int,int);
void GlobalizationNative_ChangeCaseTurkish (int,int,int,int,int);
void GlobalizationNative_CloseSortHandle (int);
int GlobalizationNative_CompareString (int,int,int,int,int,int);
int GlobalizationNative_EndsWith (int,int,int,int,int,int,int);
int GlobalizationNative_EnumCalendarInfo (int,int,int,int,int);
int GlobalizationNative_GetCalendarInfo (int,int,int,int,int);
int GlobalizationNative_GetCalendars (int,int,int);
int GlobalizationNative_GetDefaultLocaleName (int,int);
int GlobalizationNative_GetJapaneseEraStartDate (int,int,int,int);
int GlobalizationNative_GetLatestJapaneseEra ();
int GlobalizationNative_GetLocaleInfoGroupingSizes (int,int,int,int);
int GlobalizationNative_GetLocaleInfoInt (int,int,int);
int GlobalizationNative_GetLocaleInfoString (int,int,int,int,int);
int GlobalizationNative_GetLocaleName (int,int,int);
int GlobalizationNative_GetLocales (int,int);
int GlobalizationNative_GetLocaleTimeFormat (int,int,int,int);
int GlobalizationNative_GetSortHandle (int,int);
int GlobalizationNative_GetSortKey (int,int,int,int,int,int);
int GlobalizationNative_IndexOf (int,int,int,int,int,int,int);
void GlobalizationNative_InitICUFunctions (int,int,int,int);
void GlobalizationNative_InitOrdinalCasingPage (int,int);
int GlobalizationNative_IsNormalized (int,int,int);
int GlobalizationNative_IsPredefinedLocale (int);
int GlobalizationNative_LastIndexOf (int,int,int,int,int,int,int);
int GlobalizationNative_LoadICU ();
int GlobalizationNative_NormalizeString (int,int,int,int,int);
int GlobalizationNative_StartsWith (int,int,int,int,int,int,int);
int GlobalizationNative_ToAscii (int,int,int,int,int);
int GlobalizationNative_ToUnicode (int,int,int,int,int);
int sqlite3_aggregate_context (int,int);
int sqlite3_aggregate_count (int);
int sqlite3_backup_finish (int);
int sqlite3_backup_init (int,int,int,int);
int sqlite3_backup_pagecount (int);
int sqlite3_backup_remaining (int);
int sqlite3_backup_step (int,int);
int sqlite3_bind_blob (int,int,int,int,int);
int sqlite3_bind_double (int,int,double);
int sqlite3_bind_int (int,int,int);
int sqlite3_bind_int64 (int,int,int64_t);
int sqlite3_bind_null (int,int);
int sqlite3_bind_parameter_count (int);
int sqlite3_bind_parameter_index (int,int);
int sqlite3_bind_parameter_name (int,int);
int sqlite3_bind_text (int,int,int,int,int);
int sqlite3_bind_text16 (int,int,int,int,int);
int sqlite3_bind_zeroblob (int,int,int);
int sqlite3_blob_bytes (int);
int sqlite3_blob_close (int);
int sqlite3_blob_open (int,int,int,int,int64_t,int,int);
int sqlite3_blob_read (int,int,int,int);
int sqlite3_blob_reopen (int,int64_t);
int sqlite3_blob_write (int,int,int,int);
int sqlite3_busy_timeout (int,int);
int sqlite3_changes (int);
int sqlite3_clear_bindings (int);
int sqlite3_close (int);
int sqlite3_close_v2 (int);
int sqlite3_column_blob (int,int);
int sqlite3_column_bytes (int,int);
int sqlite3_column_count (int);
int sqlite3_column_database_name (int,int);
int sqlite3_column_decltype (int,int);
double sqlite3_column_double (int,int);
int sqlite3_column_int (int,int);
int64_t sqlite3_column_int64 (int,int);
int sqlite3_column_name (int,int);
int sqlite3_column_origin_name (int,int);
int sqlite3_column_table_name (int,int);
int sqlite3_column_text (int,int);
int sqlite3_column_type (int,int);
int sqlite3_commit_hook (int,int,int);
int sqlite3_compileoption_get (int);
int sqlite3_compileoption_used (int);
int sqlite3_complete (int);
int sqlite3_create_collation (int,int,int,int,int);
int sqlite3_create_function_v2 (int,int,int,int,int,int,int,int,int);
int sqlite3_data_count (int);
int sqlite3_db_filename (int,int);
int sqlite3_db_handle (int);
int sqlite3_db_readonly (int,int);
int sqlite3_db_status (int,int,int,int,int);
int sqlite3_deserialize (int,int,int,int64_t,int64_t,int);
int sqlite3_enable_load_extension (int,int);
int sqlite3_enable_shared_cache (int);
int sqlite3_errcode (int);
int sqlite3_errmsg (int);
int sqlite3_errstr (int);
int sqlite3_exec (int,int,int,int,int);
int sqlite3_extended_errcode (int);
int sqlite3_extended_result_codes (int,int);
int sqlite3_file_control (int,int,int,int);
int sqlite3_finalize (int);
void sqlite3_free (int);
int sqlite3_get_autocommit (int);
int64_t sqlite3_hard_heap_limit64 (int64_t);
int sqlite3_initialize ();
void sqlite3_interrupt (int);
int sqlite3_key (int,int,int);
int sqlite3_key_v2 (int,int,int,int);
int sqlite3_keyword_count ();
int sqlite3_keyword_name (int,int,int);
int64_t sqlite3_last_insert_rowid (int);
int sqlite3_libversion ();
int sqlite3_libversion_number ();
int sqlite3_limit (int,int,int);
int sqlite3_load_extension (int,int,int,int);
void sqlite3_log (int,int);
int sqlite3_malloc (int);
int sqlite3_malloc64 (int64_t);
int64_t sqlite3_memory_highwater (int);
int64_t sqlite3_memory_used ();
int sqlite3_next_stmt (int,int);
int sqlite3_open (int,int);
int sqlite3_open_v2 (int,int,int,int);
int sqlite3_prepare_v2 (int,int,int,int,int);
int sqlite3_prepare_v3 (int,int,int,int,int,int);
int sqlite3_profile (int,int,int);
void sqlite3_progress_handler (int,int,int,int);
int sqlite3_realloc (int,int);
int sqlite3_rekey (int,int,int);
int sqlite3_rekey_v2 (int,int,int,int);
int sqlite3_reset (int);
void sqlite3_result_blob (int,int,int,int);
void sqlite3_result_double (int,double);
void sqlite3_result_error (int,int,int);
void sqlite3_result_error_code (int,int);
void sqlite3_result_error_nomem (int);
void sqlite3_result_error_toobig (int);
void sqlite3_result_int (int,int);
void sqlite3_result_int64 (int,int64_t);
void sqlite3_result_null (int);
void sqlite3_result_text (int,int,int,int);
void sqlite3_result_zeroblob (int,int);
int sqlite3_rollback_hook (int,int,int);
int sqlite3_serialize (int,int,int,int);
int sqlite3_set_authorizer (int,int,int);
int sqlite3_shutdown ();
int sqlite3_snapshot_cmp (int,int);
void sqlite3_snapshot_free (int);
int sqlite3_snapshot_get (int,int,int);
int sqlite3_snapshot_open (int,int,int);
int sqlite3_snapshot_recover (int,int);
int64_t sqlite3_soft_heap_limit64 (int64_t);
int sqlite3_sourceid ();
int sqlite3_sql (int);
int sqlite3_status (int,int,int,int);
int sqlite3_step (int);
int sqlite3_stmt_busy (int);
int sqlite3_stmt_isexplain (int);
int sqlite3_stmt_readonly (int);
int sqlite3_stmt_status (int,int,int);
int sqlite3_stricmp (int,int);
int sqlite3_strnicmp (int,int,int);
int sqlite3_table_column_metadata (int,int,int,int,int,int,int,int,int);
int sqlite3_threadsafe ();
int sqlite3_total_changes (int);
int sqlite3_trace (int,int,int);
int sqlite3_update_hook (int,int,int);
int sqlite3_user_data (int);
int sqlite3_value_blob (int);
int sqlite3_value_bytes (int);
double sqlite3_value_double (int);
int sqlite3_value_int (int);
int64_t sqlite3_value_int64 (int);
int sqlite3_value_text (int);
int sqlite3_value_type (int);
int sqlite3_vfs_find (int);
int sqlite3_wal_autocheckpoint (int,int);
int sqlite3_wal_checkpoint (int,int);
int sqlite3_wal_checkpoint_v2 (int,int,int,int,int);
int SystemNative_Close (int);
int SystemNative_CloseDir (int);
int SystemNative_ConvertErrorPalToPlatform (int);
int SystemNative_ConvertErrorPlatformToPal (int);
int SystemNative_Dup (int);
int SystemNative_FAllocate (int,int64_t,int64_t);
int SystemNative_FLock (int,int);
void SystemNative_Free (int);
void SystemNative_FreeEnviron (int);
int SystemNative_FStat (int,int);
int SystemNative_FSync (int);
int SystemNative_FTruncate (int,int64_t);
int SystemNative_GetCryptographicallySecureRandomBytes (int,int);
int SystemNative_GetCwd (int,int);
int SystemNative_GetEnv (int);
int SystemNative_GetEnviron ();
int SystemNative_GetErrNo ();
int SystemNative_GetFileSystemType (int);
void SystemNative_GetNonCryptographicallySecureRandomBytes (int,int);
int SystemNative_GetReadDirRBufferSize ();
int64_t SystemNative_GetSystemTimeAsTicks ();
uint64_t SystemNative_GetTimestamp ();
int SystemNative_GetTimeZoneData (int,int);
void SystemNative_LowLevelMonitor_Acquire (int);
int SystemNative_LowLevelMonitor_Create ();
void SystemNative_LowLevelMonitor_Destroy (int);
void SystemNative_LowLevelMonitor_Release (int);
void SystemNative_LowLevelMonitor_Signal_Release (int);
int SystemNative_LowLevelMonitor_TimedWait (int,int);
void SystemNative_LowLevelMonitor_Wait (int);
int64_t SystemNative_LSeek (int,int64_t,int);
int SystemNative_LStat (int,int);
int SystemNative_Malloc (int);
int SystemNative_Open (int,int,int);
int SystemNative_OpenDir (int);
int SystemNative_PosixFAdvise (int,int64_t,int64_t,int);
int SystemNative_PRead (int,int,int,int64_t);
int64_t SystemNative_PReadV (int,int,int,int64_t);
int SystemNative_PWrite (int,int,int,int64_t);
int64_t SystemNative_PWriteV (int,int,int,int64_t);
int SystemNative_Read (int,int,int);
int SystemNative_ReadDirR (int,int,int,int);
int SystemNative_ReadLink (int,int,int);
int SystemNative_SchedGetCpu ();
void SystemNative_SetErrNo (int);
int SystemNative_Stat (int,int);
int SystemNative_StrErrorR (int,int,int);
int SystemNative_Unlink (int);
int SystemNative_Write (int,int,int);
static PinvokeImport e_sqlite3_imports [] = {
{"sqlite3_aggregate_context", sqlite3_aggregate_context}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_aggregate_count", sqlite3_aggregate_count}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_backup_finish", sqlite3_backup_finish}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_backup_init", sqlite3_backup_init}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_backup_pagecount", sqlite3_backup_pagecount}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_backup_remaining", sqlite3_backup_remaining}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_backup_step", sqlite3_backup_step}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_bind_blob", sqlite3_bind_blob}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_bind_double", sqlite3_bind_double}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_bind_int", sqlite3_bind_int}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_bind_int64", sqlite3_bind_int64}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_bind_null", sqlite3_bind_null}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_bind_parameter_count", sqlite3_bind_parameter_count}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_bind_parameter_index", sqlite3_bind_parameter_index}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_bind_parameter_name", sqlite3_bind_parameter_name}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_bind_text", sqlite3_bind_text}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_bind_text16", sqlite3_bind_text16}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_bind_zeroblob", sqlite3_bind_zeroblob}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_blob_bytes", sqlite3_blob_bytes}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_blob_close", sqlite3_blob_close}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_blob_open", sqlite3_blob_open}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_blob_read", sqlite3_blob_read}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_blob_reopen", sqlite3_blob_reopen}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_blob_write", sqlite3_blob_write}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_busy_timeout", sqlite3_busy_timeout}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_changes", sqlite3_changes}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_clear_bindings", sqlite3_clear_bindings}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_close", sqlite3_close}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_close_v2", sqlite3_close_v2}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_column_blob", sqlite3_column_blob}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_column_bytes", sqlite3_column_bytes}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_column_count", sqlite3_column_count}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_column_database_name", sqlite3_column_database_name}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_column_decltype", sqlite3_column_decltype}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_column_double", sqlite3_column_double}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_column_int", sqlite3_column_int}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_column_int64", sqlite3_column_int64}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_column_name", sqlite3_column_name}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_column_origin_name", sqlite3_column_origin_name}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_column_table_name", sqlite3_column_table_name}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_column_text", sqlite3_column_text}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_column_type", sqlite3_column_type}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_commit_hook", sqlite3_commit_hook}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_compileoption_get", sqlite3_compileoption_get}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_compileoption_used", sqlite3_compileoption_used}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_complete", sqlite3_complete}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_create_collation", sqlite3_create_collation}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_create_function_v2", sqlite3_create_function_v2}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_data_count", sqlite3_data_count}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_db_filename", sqlite3_db_filename}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_db_handle", sqlite3_db_handle}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_db_readonly", sqlite3_db_readonly}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_db_status", sqlite3_db_status}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_deserialize", sqlite3_deserialize}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_enable_load_extension", sqlite3_enable_load_extension}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_enable_shared_cache", sqlite3_enable_shared_cache}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_errcode", sqlite3_errcode}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_errmsg", sqlite3_errmsg}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_errstr", sqlite3_errstr}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_exec", sqlite3_exec}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_extended_errcode", sqlite3_extended_errcode}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_extended_result_codes", sqlite3_extended_result_codes}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_file_control", sqlite3_file_control}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_finalize", sqlite3_finalize}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_free", sqlite3_free}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_get_autocommit", sqlite3_get_autocommit}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_hard_heap_limit64", sqlite3_hard_heap_limit64}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_initialize", sqlite3_initialize}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_interrupt", sqlite3_interrupt}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_key", sqlite3_key}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_key_v2", sqlite3_key_v2}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_keyword_count", sqlite3_keyword_count}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_keyword_name", sqlite3_keyword_name}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_last_insert_rowid", sqlite3_last_insert_rowid}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_libversion", sqlite3_libversion}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_libversion_number", sqlite3_libversion_number}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_limit", sqlite3_limit}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_load_extension", sqlite3_load_extension}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_log", sqlite3_log}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_malloc", sqlite3_malloc}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_malloc64", sqlite3_malloc64}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_memory_highwater", sqlite3_memory_highwater}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_memory_used", sqlite3_memory_used}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_next_stmt", sqlite3_next_stmt}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_open", sqlite3_open}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_open_v2", sqlite3_open_v2}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_prepare_v2", sqlite3_prepare_v2}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_prepare_v3", sqlite3_prepare_v3}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_profile", sqlite3_profile}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_progress_handler", sqlite3_progress_handler}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_realloc", sqlite3_realloc}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_rekey", sqlite3_rekey}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_rekey_v2", sqlite3_rekey_v2}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_reset", sqlite3_reset}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_result_blob", sqlite3_result_blob}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_result_double", sqlite3_result_double}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_result_error", sqlite3_result_error}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_result_error_code", sqlite3_result_error_code}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_result_error_nomem", sqlite3_result_error_nomem}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_result_error_toobig", sqlite3_result_error_toobig}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_result_int", sqlite3_result_int}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_result_int64", sqlite3_result_int64}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_result_null", sqlite3_result_null}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_result_text", sqlite3_result_text}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_result_zeroblob", sqlite3_result_zeroblob}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_rollback_hook", sqlite3_rollback_hook}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_serialize", sqlite3_serialize}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_set_authorizer", sqlite3_set_authorizer}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_shutdown", sqlite3_shutdown}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_snapshot_cmp", sqlite3_snapshot_cmp}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_snapshot_free", sqlite3_snapshot_free}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_snapshot_get", sqlite3_snapshot_get}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_snapshot_open", sqlite3_snapshot_open}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_snapshot_recover", sqlite3_snapshot_recover}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_soft_heap_limit64", sqlite3_soft_heap_limit64}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_sourceid", sqlite3_sourceid}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_sql", sqlite3_sql}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_status", sqlite3_status}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_step", sqlite3_step}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_stmt_busy", sqlite3_stmt_busy}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_stmt_isexplain", sqlite3_stmt_isexplain}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_stmt_readonly", sqlite3_stmt_readonly}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_stmt_status", sqlite3_stmt_status}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_stricmp", sqlite3_stricmp}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_strnicmp", sqlite3_strnicmp}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_table_column_metadata", sqlite3_table_column_metadata}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_threadsafe", sqlite3_threadsafe}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_total_changes", sqlite3_total_changes}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_trace", sqlite3_trace}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_update_hook", sqlite3_update_hook}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_user_data", sqlite3_user_data}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_value_blob", sqlite3_value_blob}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_value_bytes", sqlite3_value_bytes}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_value_double", sqlite3_value_double}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_value_int", sqlite3_value_int}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_value_int64", sqlite3_value_int64}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_value_text", sqlite3_value_text}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_value_type", sqlite3_value_type}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_vfs_find", sqlite3_vfs_find}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_wal_autocheckpoint", sqlite3_wal_autocheckpoint}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_wal_checkpoint", sqlite3_wal_checkpoint}, // SQLitePCLRaw.provider.e_sqlite3
{"sqlite3_wal_checkpoint_v2", sqlite3_wal_checkpoint_v2}, // SQLitePCLRaw.provider.e_sqlite3
{NULL, NULL}
};
static PinvokeImport libSystem_Native_imports [] = {
{"SystemNative_Close", SystemNative_Close}, // System.Private.CoreLib
{"SystemNative_CloseDir", SystemNative_CloseDir}, // System.Private.CoreLib
{"SystemNative_ConvertErrorPalToPlatform", SystemNative_ConvertErrorPalToPlatform}, // System.Console, System.Net.Primitives, System.Private.CoreLib
{"SystemNative_ConvertErrorPlatformToPal", SystemNative_ConvertErrorPlatformToPal}, // System.Console, System.Private.CoreLib
{"SystemNative_Dup", SystemNative_Dup}, // System.Console
{"SystemNative_FAllocate", SystemNative_FAllocate}, // System.Private.CoreLib
{"SystemNative_FLock", SystemNative_FLock}, // System.Private.CoreLib
{"SystemNative_Free", SystemNative_Free}, // System.Private.CoreLib
{"SystemNative_FreeEnviron", SystemNative_FreeEnviron}, // System.Private.CoreLib
{"SystemNative_FStat", SystemNative_FStat}, // System.Private.CoreLib
{"SystemNative_FSync", SystemNative_FSync}, // System.Private.CoreLib
{"SystemNative_FTruncate", SystemNative_FTruncate}, // System.Private.CoreLib
{"SystemNative_GetCryptographicallySecureRandomBytes", SystemNative_GetCryptographicallySecureRandomBytes}, // System.Private.CoreLib
{"SystemNative_GetCwd", SystemNative_GetCwd}, // System.Private.CoreLib
{"SystemNative_GetEnv", SystemNative_GetEnv}, // System.Private.CoreLib
{"SystemNative_GetEnviron", SystemNative_GetEnviron}, // System.Private.CoreLib
{"SystemNative_GetErrNo", SystemNative_GetErrNo}, // System.Private.CoreLib
{"SystemNative_GetFileSystemType", SystemNative_GetFileSystemType}, // System.Private.CoreLib
{"SystemNative_GetNonCryptographicallySecureRandomBytes", SystemNative_GetNonCryptographicallySecureRandomBytes}, // System.Private.CoreLib
{"SystemNative_GetReadDirRBufferSize", SystemNative_GetReadDirRBufferSize}, // System.Private.CoreLib
{"SystemNative_GetSystemTimeAsTicks", SystemNative_GetSystemTimeAsTicks}, // System.Private.CoreLib
{"SystemNative_GetTimestamp", SystemNative_GetTimestamp}, // System.Private.CoreLib
{"SystemNative_GetTimeZoneData", SystemNative_GetTimeZoneData}, // System.Private.CoreLib
{"SystemNative_LowLevelMonitor_Acquire", SystemNative_LowLevelMonitor_Acquire}, // System.Private.CoreLib
{"SystemNative_LowLevelMonitor_Create", SystemNative_LowLevelMonitor_Create}, // System.Private.CoreLib
{"SystemNative_LowLevelMonitor_Destroy", SystemNative_LowLevelMonitor_Destroy}, // System.Private.CoreLib
{"SystemNative_LowLevelMonitor_Release", SystemNative_LowLevelMonitor_Release}, // System.Private.CoreLib
{"SystemNative_LowLevelMonitor_Signal_Release", SystemNative_LowLevelMonitor_Signal_Release}, // System.Private.CoreLib
{"SystemNative_LowLevelMonitor_TimedWait", SystemNative_LowLevelMonitor_TimedWait}, // System.Private.CoreLib
{"SystemNative_LowLevelMonitor_Wait", SystemNative_LowLevelMonitor_Wait}, // System.Private.CoreLib
{"SystemNative_LSeek", SystemNative_LSeek}, // System.Private.CoreLib
{"SystemNative_LStat", SystemNative_LStat}, // System.Private.CoreLib
{"SystemNative_Malloc", SystemNative_Malloc}, // System.Private.CoreLib
{"SystemNative_Open", SystemNative_Open}, // System.Private.CoreLib
{"SystemNative_OpenDir", SystemNative_OpenDir}, // System.Private.CoreLib
{"SystemNative_PosixFAdvise", SystemNative_PosixFAdvise}, // System.Private.CoreLib
{"SystemNative_PRead", SystemNative_PRead}, // System.Private.CoreLib
{"SystemNative_PReadV", SystemNative_PReadV}, // System.Private.CoreLib
{"SystemNative_PWrite", SystemNative_PWrite}, // System.Private.CoreLib
{"SystemNative_PWriteV", SystemNative_PWriteV}, // System.Private.CoreLib
{"SystemNative_Read", SystemNative_Read}, // System.Private.CoreLib
{"SystemNative_ReadDirR", SystemNative_ReadDirR}, // System.Private.CoreLib
{"SystemNative_ReadLink", SystemNative_ReadLink}, // System.Private.CoreLib
{"SystemNative_SchedGetCpu", SystemNative_SchedGetCpu}, // System.Private.CoreLib
{"SystemNative_SetErrNo", SystemNative_SetErrNo}, // System.Private.CoreLib
{"SystemNative_Stat", SystemNative_Stat}, // System.Private.CoreLib
{"SystemNative_StrErrorR", SystemNative_StrErrorR}, // System.Console, System.Net.Primitives, System.Private.CoreLib
{"SystemNative_Unlink", SystemNative_Unlink}, // System.Private.CoreLib
{"SystemNative_Write", SystemNative_Write}, // System.Console, System.Private.CoreLib
{NULL, NULL}
};
static PinvokeImport libSystem_IO_Compression_Native_imports [] = {
{NULL, NULL}
};
static PinvokeImport libSystem_Globalization_Native_imports [] = {
{"GlobalizationNative_ChangeCase", GlobalizationNative_ChangeCase}, // System.Private.CoreLib
{"GlobalizationNative_ChangeCaseInvariant", GlobalizationNative_ChangeCaseInvariant}, // System.Private.CoreLib
{"GlobalizationNative_ChangeCaseTurkish", GlobalizationNative_ChangeCaseTurkish}, // System.Private.CoreLib
{"GlobalizationNative_CloseSortHandle", GlobalizationNative_CloseSortHandle}, // System.Private.CoreLib
{"GlobalizationNative_CompareString", GlobalizationNative_CompareString}, // System.Private.CoreLib
{"GlobalizationNative_EndsWith", GlobalizationNative_EndsWith}, // System.Private.CoreLib
{"GlobalizationNative_EnumCalendarInfo", GlobalizationNative_EnumCalendarInfo}, // System.Private.CoreLib
{"GlobalizationNative_GetCalendarInfo", GlobalizationNative_GetCalendarInfo}, // System.Private.CoreLib
{"GlobalizationNative_GetCalendars", GlobalizationNative_GetCalendars}, // System.Private.CoreLib
{"GlobalizationNative_GetDefaultLocaleName", GlobalizationNative_GetDefaultLocaleName}, // System.Private.CoreLib
{"GlobalizationNative_GetJapaneseEraStartDate", GlobalizationNative_GetJapaneseEraStartDate}, // System.Private.CoreLib
{"GlobalizationNative_GetLatestJapaneseEra", GlobalizationNative_GetLatestJapaneseEra}, // System.Private.CoreLib
{"GlobalizationNative_GetLocaleInfoGroupingSizes", GlobalizationNative_GetLocaleInfoGroupingSizes}, // System.Private.CoreLib
{"GlobalizationNative_GetLocaleInfoInt", GlobalizationNative_GetLocaleInfoInt}, // System.Private.CoreLib
{"GlobalizationNative_GetLocaleInfoString", GlobalizationNative_GetLocaleInfoString}, // System.Private.CoreLib
{"GlobalizationNative_GetLocaleName", GlobalizationNative_GetLocaleName}, // System.Private.CoreLib
{"GlobalizationNative_GetLocales", GlobalizationNative_GetLocales}, // System.Private.CoreLib
{"GlobalizationNative_GetLocaleTimeFormat", GlobalizationNative_GetLocaleTimeFormat}, // System.Private.CoreLib
{"GlobalizationNative_GetSortHandle", GlobalizationNative_GetSortHandle}, // System.Private.CoreLib
{"GlobalizationNative_GetSortKey", GlobalizationNative_GetSortKey}, // System.Private.CoreLib
{"GlobalizationNative_IndexOf", GlobalizationNative_IndexOf}, // System.Private.CoreLib
{"GlobalizationNative_InitICUFunctions", GlobalizationNative_InitICUFunctions}, // System.Private.CoreLib
{"GlobalizationNative_InitOrdinalCasingPage", GlobalizationNative_InitOrdinalCasingPage}, // System.Private.CoreLib
{"GlobalizationNative_IsNormalized", GlobalizationNative_IsNormalized}, // System.Private.CoreLib
{"GlobalizationNative_IsPredefinedLocale", GlobalizationNative_IsPredefinedLocale}, // System.Private.CoreLib
{"GlobalizationNative_LastIndexOf", GlobalizationNative_LastIndexOf}, // System.Private.CoreLib
{"GlobalizationNative_LoadICU", GlobalizationNative_LoadICU}, // System.Private.CoreLib
{"GlobalizationNative_NormalizeString", GlobalizationNative_NormalizeString}, // System.Private.CoreLib
{"GlobalizationNative_StartsWith", GlobalizationNative_StartsWith}, // System.Private.CoreLib
{"GlobalizationNative_ToAscii", GlobalizationNative_ToAscii}, // System.Private.CoreLib
{"GlobalizationNative_ToUnicode", GlobalizationNative_ToUnicode}, // System.Private.CoreLib
{NULL, NULL}
};
static void *pinvoke_tables[] = { e_sqlite3_imports,libSystem_Native_imports,libSystem_IO_Compression_Native_imports,libSystem_Globalization_Native_imports,};
static char *pinvoke_names[] = { "e_sqlite3","libSystem.Native","libSystem.IO.Compression.Native","libSystem.Globalization.Native",};
InterpFtnDesc wasm_native_to_interp_ftndescs[17];
typedef void  (*WasmInterpEntrySig_0) (int*,int*,int*,int*,int*,int*);
int wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_exec_hook_bridge_impl (int arg0,int arg1,int arg2,int arg3) { 
int res;
((WasmInterpEntrySig_0)wasm_native_to_interp_ftndescs [0].func) ((int*)&res, (int*)&arg0, (int*)&arg1, (int*)&arg2, (int*)&arg3, wasm_native_to_interp_ftndescs [0].arg);
return res;
}
typedef void  (*WasmInterpEntrySig_1) (int*,int*,int*);
int wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_commit_hook_bridge_impl (int arg0) { 
int res;
((WasmInterpEntrySig_1)wasm_native_to_interp_ftndescs [1].func) ((int*)&res, (int*)&arg0, wasm_native_to_interp_ftndescs [1].arg);
return res;
}
typedef void  (*WasmInterpEntrySig_2) (int*,int*,int*,int*);
void wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_scalar_function_hook_bridge_impl (int arg0,int arg1,int arg2) { 
((WasmInterpEntrySig_2)wasm_native_to_interp_ftndescs [2].func) ((int*)&arg0, (int*)&arg1, (int*)&arg2, wasm_native_to_interp_ftndescs [2].arg);
}
typedef void  (*WasmInterpEntrySig_3) (int*,int*,int*,int*);
void wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_log_hook_bridge_impl (int arg0,int arg1,int arg2) { 
((WasmInterpEntrySig_3)wasm_native_to_interp_ftndescs [3].func) ((int*)&arg0, (int*)&arg1, (int*)&arg2, wasm_native_to_interp_ftndescs [3].arg);
}
typedef void  (*WasmInterpEntrySig_4) (int*,int*,int*,int*);
void wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_agg_function_step_hook_bridge_impl (int arg0,int arg1,int arg2) { 
((WasmInterpEntrySig_4)wasm_native_to_interp_ftndescs [4].func) ((int*)&arg0, (int*)&arg1, (int*)&arg2, wasm_native_to_interp_ftndescs [4].arg);
}
typedef void  (*WasmInterpEntrySig_5) (int*,int*);
void wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_agg_function_final_hook_bridge_impl (int arg0) { 
((WasmInterpEntrySig_5)wasm_native_to_interp_ftndescs [5].func) ((int*)&arg0, wasm_native_to_interp_ftndescs [5].arg);
}
typedef void  (*WasmInterpEntrySig_6) (int*,int*,int*,int*,int*,int*,int*);
int wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_collation_hook_bridge_impl (int arg0,int arg1,int arg2,int arg3,int arg4) { 
int res;
((WasmInterpEntrySig_6)wasm_native_to_interp_ftndescs [6].func) ((int*)&res, (int*)&arg0, (int*)&arg1, (int*)&arg2, (int*)&arg3, (int*)&arg4, wasm_native_to_interp_ftndescs [6].arg);
return res;
}
typedef void  (*WasmInterpEntrySig_7) (int*,int*,int*,int*,int*,int*);
void wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_update_hook_bridge_impl (int arg0,int arg1,int arg2,int arg3,int64_t arg4) { 
((WasmInterpEntrySig_7)wasm_native_to_interp_ftndescs [7].func) ((int*)&arg0, (int*)&arg1, (int*)&arg2, (int*)&arg3, (int*)&arg4, wasm_native_to_interp_ftndescs [7].arg);
}
typedef void  (*WasmInterpEntrySig_8) (int*,int*);
void wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_rollback_hook_bridge_impl (int arg0) { 
((WasmInterpEntrySig_8)wasm_native_to_interp_ftndescs [8].func) ((int*)&arg0, wasm_native_to_interp_ftndescs [8].arg);
}
typedef void  (*WasmInterpEntrySig_9) (int*,int*,int*);
void wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_trace_hook_bridge_impl (int arg0,int arg1) { 
((WasmInterpEntrySig_9)wasm_native_to_interp_ftndescs [9].func) ((int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [9].arg);
}
typedef void  (*WasmInterpEntrySig_10) (int*,int*,int*,int*);
void wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_profile_hook_bridge_impl (int arg0,int arg1,int64_t arg2) { 
((WasmInterpEntrySig_10)wasm_native_to_interp_ftndescs [10].func) ((int*)&arg0, (int*)&arg1, (int*)&arg2, wasm_native_to_interp_ftndescs [10].arg);
}
typedef void  (*WasmInterpEntrySig_11) (int*,int*,int*);
int wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_progress_handler_hook_bridge_impl (int arg0) { 
int res;
((WasmInterpEntrySig_11)wasm_native_to_interp_ftndescs [11].func) ((int*)&res, (int*)&arg0, wasm_native_to_interp_ftndescs [11].arg);
return res;
}
typedef void  (*WasmInterpEntrySig_12) (int*,int*,int*,int*,int*,int*,int*,int*);
int wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_authorizer_hook_bridge_impl (int arg0,int arg1,int arg2,int arg3,int arg4,int arg5) { 
int res;
((WasmInterpEntrySig_12)wasm_native_to_interp_ftndescs [12].func) ((int*)&res, (int*)&arg0, (int*)&arg1, (int*)&arg2, (int*)&arg3, (int*)&arg4, (int*)&arg5, wasm_native_to_interp_ftndescs [12].arg);
return res;
}
typedef void  (*WasmInterpEntrySig_13) (int*,int*,int*,int*,int*,int*,int*,int*);
int wasm_native_to_interp_System_Private_CoreLib_ComponentActivator_GetFunctionPointer (int arg0,int arg1,int arg2,int arg3,int arg4,int arg5) { 
int res;
((WasmInterpEntrySig_13)wasm_native_to_interp_ftndescs [13].func) ((int*)&res, (int*)&arg0, (int*)&arg1, (int*)&arg2, (int*)&arg3, (int*)&arg4, (int*)&arg5, wasm_native_to_interp_ftndescs [13].arg);
return res;
}
typedef void  (*WasmInterpEntrySig_14) (int*,int*,int*);
void wasm_native_to_interp_System_Private_CoreLib_CalendarData_EnumCalendarInfoCallback (int arg0,int arg1) { 
((WasmInterpEntrySig_14)wasm_native_to_interp_ftndescs [14].func) ((int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [14].arg);
}
typedef void  (*WasmInterpEntrySig_15) (int*);
void wasm_native_to_interp_System_Private_CoreLib_TimerQueue_TimerHandler () { 
((WasmInterpEntrySig_15)wasm_native_to_interp_ftndescs [15].func) (wasm_native_to_interp_ftndescs [15].arg);
}
typedef void  (*WasmInterpEntrySig_16) (int*);
void wasm_native_to_interp_System_Private_CoreLib_ThreadPool_BackgroundJobHandler () { 
((WasmInterpEntrySig_16)wasm_native_to_interp_ftndescs [16].func) (wasm_native_to_interp_ftndescs [16].arg);
}
static void *wasm_native_to_interp_funcs[] = { wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_exec_hook_bridge_impl,wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_commit_hook_bridge_impl,wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_scalar_function_hook_bridge_impl,wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_log_hook_bridge_impl,wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_agg_function_step_hook_bridge_impl,wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_agg_function_final_hook_bridge_impl,wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_collation_hook_bridge_impl,wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_update_hook_bridge_impl,wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_rollback_hook_bridge_impl,wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_trace_hook_bridge_impl,wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_profile_hook_bridge_impl,wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_progress_handler_hook_bridge_impl,wasm_native_to_interp_SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_authorizer_hook_bridge_impl,wasm_native_to_interp_System_Private_CoreLib_ComponentActivator_GetFunctionPointer,wasm_native_to_interp_System_Private_CoreLib_CalendarData_EnumCalendarInfoCallback,wasm_native_to_interp_System_Private_CoreLib_TimerQueue_TimerHandler,wasm_native_to_interp_System_Private_CoreLib_ThreadPool_BackgroundJobHandler,};
static const char *wasm_native_to_interp_map[] = { "SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_exec_hook_bridge_impl",
"SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_commit_hook_bridge_impl",
"SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_scalar_function_hook_bridge_impl",
"SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_log_hook_bridge_impl",
"SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_agg_function_step_hook_bridge_impl",
"SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_agg_function_final_hook_bridge_impl",
"SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_collation_hook_bridge_impl",
"SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_update_hook_bridge_impl",
"SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_rollback_hook_bridge_impl",
"SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_trace_hook_bridge_impl",
"SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_profile_hook_bridge_impl",
"SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_progress_handler_hook_bridge_impl",
"SQLitePCLRaw_provider_e_sqlite3_SQLite3Provider_e_sqlite3_authorizer_hook_bridge_impl",
"System_Private_CoreLib_ComponentActivator_GetFunctionPointer",
"System_Private_CoreLib_CalendarData_EnumCalendarInfoCallback",
"System_Private_CoreLib_TimerQueue_TimerHandler",
"System_Private_CoreLib_ThreadPool_BackgroundJobHandler",
};
