/**
 * BridgeInsight ONNX Runtime Web Wrapper
 * Loads a small feedforward neural network for bridge deterioration risk prediction.
 * Runs real inference in the browser — no server round-trip required.
 */
window.OnnxBridge = {
    session: null,

    /**
     * Initialize ONNX Runtime session with the bridge risk model.
     * @param {string} modelUrl - Path to the .onnx model file
     * @returns {boolean} true if session loaded successfully
     */
    init: async function (modelUrl) {
        try {
            if (typeof ort === 'undefined') {
                console.warn('ONNX Runtime Web not loaded');
                return false;
            }

            // Set WASM paths to CDN (avoids local hosting of .wasm files)
            ort.env.wasm.wasmPaths = 'https://cdn.jsdelivr.net/npm/onnxruntime-web/dist/';

            this.session = await ort.InferenceSession.create(modelUrl, {
                executionProviders: ['wasm']
            });

            console.log('ONNX session loaded:', modelUrl);
            return true;
        } catch (e) {
            console.error('Failed to init ONNX session:', e);
            return false;
        }
    },

    /**
     * Run inference on an array of bridge feature vectors.
     * Each bridge is [normalized_age, normalized_adt, min_condition_norm, scour_flag, material_risk, truck_pct_norm]
     *
     * @param {number[][]} bridgeFeatures - Array of 6-element feature vectors
     * @returns {{ scores: number[], inferenceTimeMs: number } | null}
     */
    predict: async function (bridgeFeatures) {
        if (!this.session) {
            console.warn('ONNX session not initialized');
            return null;
        }

        try {
            var results = [];
            var startTime = performance.now();

            // Run each bridge individually (small model, negligible overhead)
            for (var i = 0; i < bridgeFeatures.length; i++) {
                var inputData = new Float32Array(bridgeFeatures[i]);
                var tensor = new ort.Tensor('float32', inputData, [1, 6]);
                var feeds = { input: tensor };

                var output = await this.session.run(feeds);
                var score = output.risk_score.data[0];
                results.push(Math.round(score * 1000) / 1000); // 3 decimal places
            }

            var endTime = performance.now();

            return {
                scores: results,
                inferenceTimeMs: Math.round((endTime - startTime) * 100) / 100,
                modelName: 'bridge-risk-v1',
                runtime: 'ONNX Runtime Web (WASM)',
                bridgeCount: bridgeFeatures.length
            };
        } catch (e) {
            console.error('ONNX inference failed:', e);
            return null;
        }
    },

    /**
     * Clean up the ONNX session.
     */
    dispose: function () {
        if (this.session) {
            this.session.release();
            this.session = null;
        }
    }
};
