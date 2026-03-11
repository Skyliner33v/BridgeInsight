/**
 * BridgeInsight Hero Background — Three.js Particle Scene
 * Subtle floating wireframe geometries with mouse parallax.
 * Degrades gracefully: respects prefers-reduced-motion, reduces particles on mobile,
 * silently exits if THREE.js fails to load.
 */
window.ThreeHero = {
    scene: null,
    camera: null,
    renderer: null,
    animationId: null,
    particles: [],
    mouseX: 0,
    mouseY: 0,
    _onMouseMove: null,
    _onResize: null,

    init: function (containerId) {
        // Guard: exit silently if Three.js didn't load or WebGL unavailable
        if (typeof THREE === 'undefined') return;

        var container = document.getElementById(containerId);
        if (!container) return;

        // Respect user preference for reduced motion
        if (window.matchMedia('(prefers-reduced-motion: reduce)').matches) return;

        // Mobile detection — reduce particle count
        var isMobile = window.innerWidth < 768;

        var width = container.offsetWidth;
        var height = container.offsetHeight;
        if (width === 0 || height === 0) return;

        // Scene
        this.scene = new THREE.Scene();

        // Camera
        this.camera = new THREE.PerspectiveCamera(60, width / height, 0.1, 1000);
        this.camera.position.z = 30;

        // Renderer — transparent background so hero gradient shows through
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
        canvas.style.pointerEvents = 'none'; // Hero buttons remain clickable
        container.appendChild(canvas);

        // Color palette from design system
        var accentColor = 0x2A9D8F;   // --color-accent (teal)
        var primaryLight = 0x2B4F7E;  // --color-primary-light
        var accentLight = 0x3DB8A9;   // --color-accent-light

        var colors = [accentColor, primaryLight, accentLight];
        var particleCount = isMobile ? 8 : 18;

        // Create floating wireframe geometries
        for (var i = 0; i < particleCount; i++) {
            var geometry;
            var rand = Math.random();

            if (rand < 0.35) {
                // Icosahedron — most complex, used sparingly
                geometry = new THREE.IcosahedronGeometry(Math.random() * 1.2 + 0.4, 0);
            } else if (rand < 0.65) {
                // Octahedron — clean diamond shape
                geometry = new THREE.OctahedronGeometry(Math.random() * 1.0 + 0.3, 0);
            } else if (rand < 0.85) {
                // Tetrahedron — simple pyramid
                geometry = new THREE.TetrahedronGeometry(Math.random() * 0.8 + 0.3, 0);
            } else {
                // Small sphere — dots of light
                geometry = new THREE.SphereGeometry(Math.random() * 0.3 + 0.1, 6, 4);
            }

            var material = new THREE.MeshBasicMaterial({
                color: colors[Math.floor(Math.random() * colors.length)],
                wireframe: true,
                transparent: true,
                opacity: Math.random() * 0.12 + 0.04 // Very subtle: 0.04 – 0.16
            });

            var mesh = new THREE.Mesh(geometry, material);

            // Spread across the hero area
            mesh.position.set(
                (Math.random() - 0.5) * 50,
                (Math.random() - 0.5) * 25,
                (Math.random() - 0.5) * 15
            );

            // Random initial rotation
            mesh.rotation.set(
                Math.random() * Math.PI * 2,
                Math.random() * Math.PI * 2,
                0
            );

            // Animation parameters
            mesh.userData = {
                rotSpeedX: (Math.random() - 0.5) * 0.004,
                rotSpeedY: (Math.random() - 0.5) * 0.004,
                floatSpeed: Math.random() * 0.3 + 0.15,
                floatAmp: Math.random() * 0.6 + 0.2,
                floatOffset: Math.random() * Math.PI * 2,
                originalY: mesh.position.y
            };

            this.scene.add(mesh);
            this.particles.push(mesh);
        }

        // Thin connecting lines between nearby particles (data-flow aesthetic)
        var lineCount = isMobile ? 3 : 6;
        for (var j = 0; j < lineCount && j < this.particles.length - 1; j++) {
            var p1 = this.particles[j];
            var p2 = this.particles[j + 1 + Math.floor(Math.random() * 2)];
            if (!p2) p2 = this.particles[0];

            var lineGeom = new THREE.BufferGeometry().setFromPoints([
                p1.position.clone(),
                p2.position.clone()
            ]);
            var lineMat = new THREE.LineBasicMaterial({
                color: accentColor,
                transparent: true,
                opacity: 0.06
            });
            var line = new THREE.Line(lineGeom, lineMat);
            line.userData = { p1Index: j, p2Index: (j + 1 + Math.floor(Math.random() * 2)) % this.particles.length, isLine: true };
            this.scene.add(line);
            this.particles.push(line);
        }

        // Mouse parallax listener
        var self = this;
        this._onMouseMove = function (e) {
            self.mouseX = (e.clientX / window.innerWidth - 0.5) * 2;
            self.mouseY = (e.clientY / window.innerHeight - 0.5) * 2;
        };
        window.addEventListener('mousemove', this._onMouseMove);

        // Resize handler
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

        // Animation loop
        var animate = function () {
            self.animationId = requestAnimationFrame(animate);
            var time = Date.now() * 0.001;

            for (var k = 0; k < self.particles.length; k++) {
                var p = self.particles[k];
                var ud = p.userData;

                if (ud.isLine) {
                    // Update line positions to follow their particles
                    var src = self.particles[ud.p1Index];
                    var dst = self.particles[ud.p2Index];
                    if (src && dst && p.geometry) {
                        var positions = p.geometry.attributes.position.array;
                        positions[0] = src.position.x;
                        positions[1] = src.position.y;
                        positions[2] = src.position.z;
                        positions[3] = dst.position.x;
                        positions[4] = dst.position.y;
                        positions[5] = dst.position.z;
                        p.geometry.attributes.position.needsUpdate = true;
                    }
                } else {
                    // Rotate
                    p.rotation.x += ud.rotSpeedX;
                    p.rotation.y += ud.rotSpeedY;
                    // Float gently
                    p.position.y = ud.originalY + Math.sin(time * ud.floatSpeed + ud.floatOffset) * ud.floatAmp;
                }
            }

            // Subtle camera parallax following mouse
            self.camera.position.x += (self.mouseX * 1.5 - self.camera.position.x) * 0.015;
            self.camera.position.y += (-self.mouseY * 1.0 - self.camera.position.y) * 0.015;

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
        for (var i = 0; i < this.particles.length; i++) {
            if (this.particles[i].geometry) this.particles[i].geometry.dispose();
            if (this.particles[i].material) this.particles[i].material.dispose();
        }
        this.particles = [];
        this.scene = null;
        this.camera = null;
        this.mouseX = 0;
        this.mouseY = 0;
    }
};
