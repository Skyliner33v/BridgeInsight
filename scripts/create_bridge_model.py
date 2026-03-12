"""
Create a small ONNX feedforward neural network for bridge deterioration risk prediction.

This model encodes engineering heuristics (not trained on data) as a prototype
demonstrating the ONNX-to-browser inference pipeline.

Inputs (6 float32 features):
  0: normalized_age        — (current_year - year_built) / 150, clamped 0-1
  1: normalized_adt         — average_daily_traffic / 100000, clamped 0-1
  2: min_condition_norm     — lowest_condition_rating / 9.0 (inverted: lower = worse)
  3: scour_flag             — 1.0 if scour critical, 0.0 otherwise
  4: material_risk          — risk factor by material type (0.0 = concrete, 0.3 = steel, 0.6 = timber)
  5: truck_pct_norm         — truck_traffic_percent / 100.0

Output (1 float32):
  risk_score — 0.0 (low risk) to 1.0 (critical risk)

Architecture: Input(6) → Dense(8, ReLU) → Dense(1, Sigmoid)
"""

import numpy as np
import onnx
from onnx import helper, TensorProto, numpy_helper

# --- Layer 1: Input(6) → Hidden(8) with ReLU ---
# Weights encode engineering heuristics:
#   - High age → high risk
#   - High ADT → moderate risk increase
#   - Low condition rating → high risk (input is already normalized 0-1 where 1=good)
#   - Scour → significant risk
#   - Material risk → moderate factor
#   - High truck % → moderate risk

np.random.seed(42)  # Reproducible

# W1: shape [6, 8] — each column is a hidden neuron's weights
W1 = np.array([
    # age    adt    cond   scour  matl   truck
    [ 0.8,   0.3,  -0.9,   0.7,   0.5,   0.3],  # neuron 0: general risk
    [ 0.9,   0.1,  -0.7,   0.8,   0.2,   0.1],  # neuron 1: age + scour
    [ 0.2,   0.7,  -0.5,   0.1,   0.1,   0.8],  # neuron 2: traffic load
    [ 0.1,   0.1,  -1.0,   0.3,   0.1,   0.1],  # neuron 3: condition focused
    [ 0.6,   0.2,  -0.6,   0.9,   0.4,   0.2],  # neuron 4: structural risk
    [ 0.3,   0.5,  -0.3,   0.1,   0.7,   0.5],  # neuron 5: material + traffic
    [ 0.7,   0.4,  -0.8,   0.6,   0.3,   0.4],  # neuron 6: combined
    [ 0.4,   0.3,  -0.4,   0.2,   0.2,   0.2],  # neuron 7: moderate baseline
], dtype=np.float32).T  # Transpose to [6, 8]

B1 = np.array([-0.3, -0.4, -0.3, -0.2, -0.3, -0.3, -0.4, -0.1], dtype=np.float32)

# W2: shape [8, 1] — output layer
W2 = np.array([
    [0.5],   # neuron 0
    [0.4],   # neuron 1
    [0.3],   # neuron 2
    [0.4],   # neuron 3
    [0.5],   # neuron 4
    [0.2],   # neuron 5
    [0.4],   # neuron 6
    [0.1],   # neuron 7
], dtype=np.float32)

B2 = np.array([-1.2], dtype=np.float32)  # Bias to shift sigmoid center

# --- Build ONNX graph ---

# Input
X = helper.make_tensor_value_info("input", TensorProto.FLOAT, [None, 6])

# Output
Y = helper.make_tensor_value_info("risk_score", TensorProto.FLOAT, [None, 1])

# Weight/bias initializers
w1_init = numpy_helper.from_array(W1, name="W1")
b1_init = numpy_helper.from_array(B1, name="B1")
w2_init = numpy_helper.from_array(W2, name="W2")
b2_init = numpy_helper.from_array(B2, name="B2")

# Nodes
matmul1 = helper.make_node("MatMul", ["input", "W1"], ["matmul1_out"])
add1 = helper.make_node("Add", ["matmul1_out", "B1"], ["hidden_pre"])
relu1 = helper.make_node("Relu", ["hidden_pre"], ["hidden"])
matmul2 = helper.make_node("MatMul", ["hidden", "W2"], ["matmul2_out"])
add2 = helper.make_node("Add", ["matmul2_out", "B2"], ["logit"])
sigmoid = helper.make_node("Sigmoid", ["logit"], ["risk_score"])

# Graph
graph = helper.make_graph(
    [matmul1, add1, relu1, matmul2, add2, sigmoid],
    "bridge_risk_model",
    [X],
    [Y],
    initializer=[w1_init, b1_init, w2_init, b2_init],
)

# Model
model = helper.make_model(graph, opset_imports=[helper.make_opsetid("", 13)])
model.ir_version = 8
model.doc_string = (
    "Bridge Deterioration Risk Prototype — "
    "Feedforward neural network with heuristic weights for bridge risk scoring. "
    "Inputs: normalized age, ADT, min condition rating, scour flag, material risk, truck %. "
    "Output: risk score 0.0-1.0. "
    "Created as a prototype demonstrating ONNX Runtime Web inference pipeline."
)

# Validate
onnx.checker.check_model(model)

# Save
output_path = "src/BridgeInsight/wwwroot/data/models/bridge-risk.onnx"
onnx.save(model, output_path)
print(f"Model saved to {output_path}")
print(f"Model size: {len(model.SerializeToString())} bytes")

# Quick test
import onnx.reference
ref = onnx.reference.ReferenceEvaluator(model)

# Test: old bridge, high traffic, poor condition, scour
test_input = np.array([[0.8, 0.6, 0.3, 1.0, 0.3, 0.4]], dtype=np.float32)
result = ref.run(None, {"input": test_input})
print(f"Test (high risk): {result[0][0][0]:.4f}")

# Test: new bridge, low traffic, good condition, no scour
test_input2 = np.array([[0.1, 0.1, 0.9, 0.0, 0.0, 0.1]], dtype=np.float32)
result2 = ref.run(None, {"input": test_input2})
print(f"Test (low risk):  {result2[0][0][0]:.4f}")
