/**
 * Contact Page Background — Three.js Network Scene
 * Floating nodes with connecting lines — a "network/connection" aesthetic
 * matching the contact page theme. Lighter and more spread out than the hero scene.
 */
window.ThreeContact = {
    scene: null,
    camera: null,
    renderer: null,
    animationId: null,
    nodes: [],
    lines: [],
    mouseX: 0,
    mouseY: 0,
    _onMouseMove: null,
    _onResize: null,

    init: function (containerId) {
        if (typeof THREE === 'undefined') return;

        var container = document.getElementById(containerId);
        if (!container) return;

        if (window.matchMedia('(prefers-reduced-motion: reduce)').matches) return;

        var isMobile = window.innerWidth < 768;
        var width = container.offsetWidth;
        var height = container.offsetHeight;
        if (width === 0 || height === 0) return;

        this.scene = new THREE.Scene();
        this.camera = new THREE.PerspectiveCamera(50, width / height, 0.1, 1000);
        this.camera.position.z = 40;

        this.renderer = new THREE.WebGLRenderer({ alpha: true, antialias: !isMobile });
        this.renderer.setSize(width, height);
        this.renderer.setPixelRatio(Math.min(window.devicePixelRatio, 2));
        this.renderer.setClearColor(0x000000, 0);

        var canvas = this.renderer.domElement;
        canvas.style.position = 'absolute';
        canvas.style.top = '0';
        canvas.style.left = '0';
        canvas.style.width = '100%';
        canvas.style.height = '100%';
        canvas.style.pointerEvents = 'none';
        container.appendChild(canvas);

        var accentColor = 0x2A9D8F;
        var accentLight = 0x3DB8A9;
        var white = 0xFFFFFF;
        var colors = [accentColor, accentLight, white];

        var nodeCount = isMobile ? 12 : 24;

        // Create small glowing nodes (small spheres and dots)
        for (var i = 0; i < nodeCount; i++) {
            var size = Math.random() * 0.25 + 0.08;
            var geometry = new THREE.SphereGeometry(size, 8, 6);
            var material = new THREE.MeshBasicMaterial({
                color: colors[Math.floor(Math.random() * colors.length)],
                transparent: true,
                opacity: Math.random() * 0.18 + 0.06
            });

            var mesh = new THREE.Mesh(geometry, material);
            mesh.position.set(
                (Math.random() - 0.5) * 60,
                (Math.random() - 0.5) * 20,
                (Math.random() - 0.5) * 15
            );

            mesh.userData = {
                rotSpeedX: (Math.random() - 0.5) * 0.002,
                rotSpeedY: (Math.random() - 0.5) * 0.002,
                floatSpeed: Math.random() * 0.25 + 0.1,
                floatAmp: Math.random() * 0.5 + 0.15,
                floatOffset: Math.random() * Math.PI * 2,
                originalY: mesh.position.y,
                driftX: (Math.random() - 0.5) * 0.003
            };

            this.scene.add(mesh);
            this.nodes.push(mesh);
        }

        // Create connecting lines between nearby nodes
        var lineCount = isMobile ? 6 : 14;
        var usedPairs = {};
        var linesCreated = 0;

        for (var a = 0; a < this.nodes.length && linesCreated < lineCount; a++) {
            for (var b = a + 1; b < this.nodes.length && linesCreated < lineCount; b++) {
                var dist = this.nodes[a].position.distanceTo(this.nodes[b].position);
                var pairKey = a + '-' + b;

                if (dist < 20 && dist > 5 && !usedPairs[pairKey]) {
                    usedPairs[pairKey] = true;

                    var lineGeom = new THREE.BufferGeometry().setFromPoints([
                        this.nodes[a].position.clone(),
                        this.nodes[b].position.clone()
                    ]);
                    var lineMat = new THREE.LineBasicMaterial({
                        color: accentColor,
                        transparent: true,
                        opacity: 0.05
                    });
                    var line = new THREE.Line(lineGeom, lineMat);
                    line.userData = { nodeA: a, nodeB: b };
                    this.scene.add(line);
                    this.lines.push(line);
                    linesCreated++;
                }
            }
        }

        var self = this;
        this._onMouseMove = function (e) {
            self.mouseX = (e.clientX / window.innerWidth - 0.5) * 2;
            self.mouseY = (e.clientY / window.innerHeight - 0.5) * 2;
        };
        window.addEventListener('mousemove', this._onMouseMove);

        this._onResize = function () {
            var w = container.offsetWidth;
            var h = container.offsetHeight;
            if (w > 0 && h > 0) {
                self.camera.aspect = w / h;
                self.camera.updateProjectionMatrix();
                self.renderer.setSize(w, h);
            }
        };
        window.addEventListener('resize', this._onResize);

        var animate = function () {
            self.animationId = requestAnimationFrame(animate);
            var time = Date.now() * 0.001;

            // Animate nodes
            for (var k = 0; k < self.nodes.length; k++) {
                var node = self.nodes[k];
                var ud = node.userData;
                node.rotation.x += ud.rotSpeedX;
                node.rotation.y += ud.rotSpeedY;
                node.position.y = ud.originalY + Math.sin(time * ud.floatSpeed + ud.floatOffset) * ud.floatAmp;
                node.position.x += ud.driftX;

                // Wrap around horizontally for continuous flow
                if (node.position.x > 35) node.position.x = -35;
                if (node.position.x < -35) node.position.x = 35;
            }

            // Update line positions to follow nodes
            for (var l = 0; l < self.lines.length; l++) {
                var line = self.lines[l];
                var nA = self.nodes[line.userData.nodeA];
                var nB = self.nodes[line.userData.nodeB];
                if (nA && nB && line.geometry) {
                    var pos = line.geometry.attributes.position.array;
                    pos[0] = nA.position.x; pos[1] = nA.position.y; pos[2] = nA.position.z;
                    pos[3] = nB.position.x; pos[4] = nB.position.y; pos[5] = nB.position.z;
                    line.geometry.attributes.position.needsUpdate = true;

                    // Fade lines based on node distance
                    var d = nA.position.distanceTo(nB.position);
                    line.material.opacity = d < 25 ? 0.06 * (1 - d / 25) : 0;
                }
            }

            // Subtle parallax
            self.camera.position.x += (self.mouseX * 1.2 - self.camera.position.x) * 0.012;
            self.camera.position.y += (-self.mouseY * 0.8 - self.camera.position.y) * 0.012;

            self.renderer.render(self.scene, self.camera);
        };
        animate();
    },

    dispose: function () {
        if (this.animationId) {
            cancelAnimationFrame(this.animationId);
            this.animationId = null;
        }
        if (this._onMouseMove) {
            window.removeEventListener('mousemove', this._onMouseMove);
            this._onMouseMove = null;
        }
        if (this._onResize) {
            window.removeEventListener('resize', this._onResize);
            this._onResize = null;
        }
        if (this.renderer) {
            this.renderer.dispose();
            if (this.renderer.domElement && this.renderer.domElement.parentNode) {
                this.renderer.domElement.parentNode.removeChild(this.renderer.domElement);
            }
            this.renderer = null;
        }
        var all = this.nodes.concat(this.lines);
        for (var i = 0; i < all.length; i++) {
            if (all[i].geometry) all[i].geometry.dispose();
            if (all[i].material) all[i].material.dispose();
        }
        this.nodes = [];
        this.lines = [];
        this.scene = null;
        this.camera = null;
        this.mouseX = 0;
        this.mouseY = 0;
    }
};
