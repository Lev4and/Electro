var Custombox;
(function (Custombox) {
    // Values
    const CB = 'custombox';
    const OPEN = `${CB}-open`;
    const CLOSE = `${CB}-close`;
    const LOCK = `${CB}-lock`;
    const FROM = 'animateFrom';
    const BLOCK = 'block';
    const positionValues = ['top', 'right', 'bottom', 'left'];
    // Effects
    const animationValues = ['slide', 'blur', 'flip', 'rotate', 'letmein', 'makeway', 'slip', 'corner', 'slidetogether', 'push', 'contentscale'];
    const containerValues = ['blur', 'makeway', 'slip', 'push', 'contentscale'];
    const overlayValues = ['letmein', 'makeway', 'slip', 'corner', 'slidetogether', 'door', 'push', 'contentscale'];
    const together = ['corner', 'slidetogether', 'scale', 'door', 'push', 'contentscale'];
    const perspective = ['fall', 'sidefall', 'flip', 'sign', 'slit', 'letmein', 'makeway', 'slip'];
    class Snippet {
        static check(values, match) {
            return values.indexOf(match) > -1;
        }
        static isIE() {
            const ua = window.navigator.userAgent;
            const msie = ua.indexOf('MSIE ');
            if (msie > 0) {
                // IE 10 or older => return version number
                return !isNaN(parseInt(ua.substring(msie + 5, ua.indexOf('.', msie)), 10));
            }
            const trident = ua.indexOf('Trident/');
            if (trident > 0) {
                // IE 11 => return version number
                const rv = ua.indexOf('rv:');
                return !isNaN(parseInt(ua.substring(rv + 3, ua.indexOf('.', rv)), 10));
            }
            const edge = ua.indexOf('Edge/');
            if (edge > 0) {
                // Edge (IE 12+) => return version number
                return !isNaN(parseInt(ua.substring(edge + 5, ua.indexOf('.', edge)), 10));
            }
            // other browser
            return false;
        }
    }
    class Scroll {
        constructor() {
            this.position = document.documentElement && document.documentElement.scrollTop || document.body && document.body.scrollTop || 0;
            document.documentElement.classList.add(`${CB}-perspective`);
        }
        // Public methods
        remove() {
            document.documentElement.classList.remove(`${CB}-perspective`);
            window.scrollTo(0, this.position);
        }
    }
    class DefaultSchema {
        constructor() {
            this.overlay = {
                color: '#000',
                opacity: .48,
                close: true,
                speedIn: 300,
                speedOut: 300,
                onOpen: null,
                onComplete: null,
                onClose: null,
                active: true,
            };
            this.content = {
                id: null,
                target: null,
                container: null,
                clone: false,
                animateFrom: 'top',
                animateTo: 'top',
                positionX: 'center',
                positionY: 'center',
                width: null,
                effect: 'fadein',
                speedIn: 300,
                speedOut: 300,
                delay: 150,
                fullscreen: false,
                onOpen: null,
                onComplete: null,
                onClose: null,
                close: true,
            };
            this.loader = {
                active: true,
                color: '#FFF',
                background: '#999',
                speed: 1000,
            };
        }
    }
    class Options extends DefaultSchema {
        constructor(options) {
            super();
            Object.keys(this).forEach((key) => {
                if (options[key]) {
                    Object.assign(this[key], options[key]);
                }
            });
        }
    }
    class Loader {
        constructor(options) {
            this.options = options;
            this.element = document.createElement('div');
            this.element.classList.add(`${CB}-loader`);
            this.element.style.borderColor = this.options.loader.background;
            this.element.style.borderTopColor = this.options.loader.color;
            this.element.style.animationDuration = `${this.options.loader.speed}ms`;
            document.body.appendChild(this.element);
        }
        // Public methods
        show() {
            this.element.style.display = BLOCK;
        }
        destroy() {
            this.element.parentElement.removeChild(this.element);
        }
    }
    class Container {
        constructor(options) {
            this.options = options;
            if (document.readyState === 'loading') {
                throw new Error('You need to instantiate Custombox when the document is fully loaded');
            }
            const selector = document.querySelector(this.options.content.container);
            if (selector) {
                this.element = selector;
            }
            else if (!document.querySelector(`.${CB}-container`)) {
                this.element = document.createElement('div');
                while (document.body.firstChild) {
                    this.element.appendChild(document.body.firstChild);
                }
                document.body.appendChild(this.element);
            }
            else if (document.querySelector(`.${CB}-container`)) {
                this.element = document.querySelector(`.${CB}-container`);
            }
            this.element.classList.add(`${CB}-container`);
            this.element.classList.add(`${CB}-${this.options.content.effect}`);
            this.element.style.animationDuration = `${this.options.content.speedIn}ms`;
            if (Snippet.check(animationValues, this.options.content.effect)) {
                this.setAnimation();
            }
        }
        // Public methods
        bind(method) {
            if (method === CLOSE) {
                if (Snippet.check(animationValues, this.options.content.effect)) {
                    this.setAnimation('animateTo');
                }
                this.element.classList.remove(OPEN);
            }
            this.element.classList.add(method);
            return new Promise((resolve) => this.listener().then(() => resolve()));
        }
        remove() {
            this.element.classList.remove(CLOSE);
            this.element.classList.remove(`${CB}-${this.options.content.effect}`);
            this.element.style.removeProperty('animation-duration');
            const elements = document.querySelectorAll(`.${CB}-content`);
            const container = document.querySelector(this.options.content.container);
            if (!elements.length) {
                if (container) {
                    const classes = this.element.className.split(' ');
                    for (let i = 0, t = classes.length; i < t; i++) {
                        if (classes[i].startsWith(`${CB}-`)) {
                            this.element.classList.remove(classes[i]);
                        }
                    }
                }
                else {
                    const container = document.querySelector(`.${CB}-container`);
                    while (container.firstChild)
                        container.parentNode.insertBefore(container.firstChild, container);
                    container.parentNode.removeChild(container);
                }
            }
        }
        // Private methods
        listener() {
            return new Promise((resolve) => {
                if (!Snippet.isIE()) {
                    this.element.addEventListener('animationend', () => resolve(), true);
                }
                else {
                    setTimeout(resolve, this.options.content.speedIn);
                }
            });
        }
        setAnimation(action = FROM) {
            for (let i = 0, t = positionValues.length; i < t; i++) {
                if (this.element.classList.contains(`${CB}-${positionValues[i]}`)) {
                    this.element.classList.remove(`${CB}-${positionValues[i]}`);
                }
            }
            this.element.classList.add(`${CB}-${this.options.content[action]}`);
        }
    }
    class Overlay {
        constructor(options) {
            this.options = options;
            this.element = document.createElement('div');
            this.element.style.backgroundColor = this.options.overlay.color;
            this.element.classList.add(`${CB}-overlay`);
            this.setAnimation();
        }
        // Public methods
        bind(method) {
            switch (method) {
                case CLOSE:
                    if (Snippet.check(overlayValues, this.options.content.effect)) {
                        this.toggleAnimation('animateTo');
                    }
                    this.element.classList.add(CLOSE);
                    this.element.classList.remove(OPEN);
                    break;
                default:
                    // Append
                    document.body.appendChild(this.element);
                    // Initialization
                    this.element.classList.add(`${CB}-${this.options.content.effect}`);
                    this.element.classList.add(OPEN);
                    break;
            }
            return new Promise((resolve) => this.listener().then(() => resolve()));
        }
        remove() {
            try {
                this.element.parentNode.removeChild(this.element);
                this.style.parentNode.removeChild(this.style);
            }
            catch (e) { }
        }
        // Private methods
        createSheet() {
            this.style = document.createElement('style');
            this.style.setAttribute('id', `${CB}-overlay-${Date.now()}`);
            document.head.appendChild(this.style);
            return this.style.sheet;
        }
        listener() {
            return new Promise((resolve) => {
                if (!Snippet.isIE()) {
                    this.element.addEventListener('animationend', () => resolve(), true);
                }
                else {
                    setTimeout(resolve, this.options.overlay.speedIn);
                }
            });
        }
        setAnimation() {
            let sheet = this.createSheet();
            if (Snippet.check(overlayValues, this.options.content.effect)) {
                this.element.style.opacity = this.options.overlay.opacity.toString();
                this.element.style.animationDuration = `${this.options.overlay.speedIn}ms`;
                this.toggleAnimation();
            }
            else {
                sheet.insertRule(`.${CB}-overlay { animation: CloseFade ${this.options.overlay.speedOut}ms; }`, 0);
                sheet.insertRule(`.${OPEN}.${CB}-overlay { animation: OpenFade ${this.options.overlay.speedIn}ms; opacity: ${this.options.overlay.opacity} }`, 0);
                sheet.insertRule(`@keyframes OpenFade { from {opacity: 0} to {opacity: ${this.options.overlay.opacity}} }`, 0);
                sheet.insertRule(`@keyframes CloseFade { from {opacity: ${this.options.overlay.opacity}} to {opacity: 0} }`, 0);
            }
            if (Snippet.check(together, this.options.content.effect)) {
                let duration = this.options.overlay.speedIn;
                if (Snippet.check(together, this.options.content.effect)) {
                    duration = this.options.content.speedIn;
                }
                this.element.style.animationDuration = `${duration}ms`;
            }
        }
        toggleAnimation(action = FROM) {
            for (let i = 0, t = positionValues.length; i < t; i++) {
                if (this.element.classList.contains(`${CB}-${positionValues[i]}`)) {
                    this.element.classList.remove(`${CB}-${positionValues[i]}`);
                }
            }
            this.element.classList.add(`${CB}-${this.options.content[action]}`);
        }
    }
    class Content {
        constructor(options) {
            this.options = options;
            this.element = document.createElement('div');
            this.element.style.animationDuration = `${this.options.content.speedIn}ms`;
            if (this.options.content.id) {
                this.element.setAttribute('id', `${CB}-${this.options.content.id}`);
            }
            if (!Snippet.check(together, this.options.content.effect)) {
                this.element.style.animationDelay = `${this.options.content.delay}ms`;
            }
            this.element.classList.add(`${CB}-content`);
            // Check fullscreen
            if (this.options.content.fullscreen) {
                this.element.classList.add(`${CB}-fullscreen`);
            }
            else {
                this.element.classList.add(`${CB}-x-${this.options.content.positionX}`);
                this.element.classList.add(`${CB}-y-${this.options.content.positionY}`);
            }
            if (Snippet.check(animationValues, this.options.content.effect)) {
                this.setAnimation();
            }
        }
        // Public methods
        fetch() {
            return new Promise((resolve, reject) => {
                // Youtube
                const regExp = /^.*(youtu\.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/;
                let match = this.options.content.target.match(regExp);
                if (match && match[2].length == 11) {
                    const element = document.createElement('div');
                    let frame = document.createElement('iframe');
                    frame.setAttribute('src', `https://www.youtube.com/embed/${match[2]}`);
                    frame.setAttribute('frameborder', '0');
                    frame.setAttribute('allowfullscreen', '');
                    frame.setAttribute('width', '100%');
                    frame.setAttribute('height', '100%');
                    element.appendChild(frame);
                    if (!this.options.content.fullscreen) {
                        let w = window.innerWidth > 560 ? 560 : window.innerWidth;
                        let h = window.innerHeight > 315 ? 315 : window.innerHeight;
                        const natural = parseInt(this.options.content.width, 10);
                        if (this.options.content.width && window.innerWidth > natural) {
                            h = Math.round(h * natural / w);
                            w = natural;
                        }
                        frame.setAttribute('width', `${w}px`);
                        frame.setAttribute('height', `${h}px`);
                    }
                    this.element.appendChild(element);
                    resolve();
                }
                else if (this.options.content.target.charAt(0) !== '#' && this.options.content.target.charAt(0) !== '.') {
                    const req = new XMLHttpRequest();
                    req.open('GET', this.options.content.target);
                    req.onload = () => {
                        if (req.status === 200) {
                            this.element.insertAdjacentHTML('beforeend', req.response);
                            let child = this.element.firstChild;
                            // Set visible
                            try {
                                child.style.display = BLOCK;
                            }
                            catch (e) {
                                reject(new Error('The ajax response need a wrapper element'));
                            }
                            if (this.options.content.width) {
                                child.style.flexBasis = this.options.content.width;
                            }
                            resolve();
                        }
                        else {
                            reject(new Error(req.statusText));
                        }
                    };
                    req.onerror = () => reject(new Error('Network error'));
                    req.send();
                }
                else {
                    // Selector
                    let selector = document.querySelector(this.options.content.target);
                    if (selector) {
                        if (this.options.content.clone) {
                            selector = selector.cloneNode(true);
                            selector.removeAttribute('id');
                        }
                        else {
                            this.reference = document.createElement('div');
                            this.reference.classList.add(`${CB}-reference`);
                            this.reference.setAttribute('style', selector.getAttribute('style'));
                            selector.parentNode.insertBefore(this.reference, selector.nextSibling);
                        }
                        // Set visible
                        selector.style.display = BLOCK;
                        // Set width
                        if (this.options.content.width) {
                            selector.style.flexBasis = this.options.content.width;
                        }
                        this.element.appendChild(selector);
                        resolve();
                    }
                    else {
                        reject(new Error(`The element doesn't exist`));
                    }
                }
            });
        }
        bind(method) {
            switch (method) {
                case CLOSE:
                    this.element.style.animationDelay = '0ms';
                    this.element.style.animationDuration = `${this.options.content.speedOut}ms`;
                    this.element.classList.remove(OPEN);
                    this.element.classList.add(CLOSE);
                    this.setAnimation('animateTo');
                    break;
                default:
                    // Append
                    document.body.appendChild(this.element);
                    // Initialization
                    this.element.classList.add(`${CB}-${this.options.content.effect}`);
                    this.element.classList.add(OPEN);
                    break;
            }
            return new Promise((resolve) => this.listener().then(() => resolve()));
        }
        remove() {
            const match = new RegExp('^[#|.]');
            if (!this.options.content.clone && match.test(this.options.content.target)) {
                const element = this.element.childNodes[0];
                element.setAttribute('style', this.reference.getAttribute('style'));
                this.reference.parentNode.insertBefore(this.element.childNodes[0], this.reference.nextSibling);
                this.reference.parentNode.removeChild(this.reference);
            }
            try {
                this.element.parentNode.removeChild(this.element);
            }
            catch (e) { }
        }
        // Private methods
        listener() {
            return new Promise((resolve) => {
                if (!Snippet.isIE()) {
                    this.element.addEventListener('animationend', () => resolve(), true);
                }
                else {
                    setTimeout(resolve, this.options.content.speedIn);
                }
            });
        }
        setAnimation(action = FROM) {
            for (let i = 0, t = positionValues.length; i < t; i++) {
                if (this.element.classList.contains(`${CB}-${positionValues[i]}`)) {
                    this.element.classList.remove(`${CB}-${positionValues[i]}`);
                }
            }
            this.element.classList.add(`${CB}-${this.options.content[action]}`);
        }
    }
    class modal {
        constructor(options) {
            this.action = (event) => {
                if (event.keyCode === 27) {
                    Custombox.modal.close();
                }
            };
            this.options = new Options(options);
        }
        // Public methods
        open() {
            this.build();
            if (this.options.loader.active) {
                this.loader.show();
            }
            this.content
                .fetch()
                .then(() => {
                // Scroll
                if (Snippet.check(perspective, this.options.content.effect)) {
                    this.scroll = new Scroll();
                }
                // Overlay
                if (this.options.overlay.active) {
                    this.dispatchEvent('overlay.onOpen');
                    this.overlay
                        .bind(OPEN)
                        .then(() => {
                        this.dispatchEvent('overlay.onComplete');
                        if (this.options.loader.active) {
                            this.loader.destroy();
                        }
                    });
                }
                else if (this.options.loader.active) {
                    this.loader.destroy();
                }
                // Container
                if (this.container) {
                    this.container.bind(OPEN);
                }
                // Content
                document.body.classList.add(LOCK);
                this.content.bind(OPEN).then(() => this.dispatchEvent('content.onComplete'));
                // Dispatch event
                this.dispatchEvent('content.onOpen');
                // Listeners
                this.listeners();
            })
                .catch((error) => {
                if (this.options.loader.active) {
                    this.loader.destroy();
                }
                throw error;
            });
        }
        // Private methods
        build() {
            // Create loader
            if (this.options.loader.active) {
                this.loader = new Loader(this.options);
            }
            // Create container
            if (Snippet.check(containerValues, this.options.content.effect)) {
                this.container = new Container(this.options);
            }
            // Create overlay
            if (this.options.overlay.active) {
                this.overlay = new Overlay(this.options);
            }
            // Create content
            this.content = new Content(this.options);
        }
        static close(id) {
            const event = new CustomEvent(`${CB}:close`);
            let elements = document.querySelectorAll(`.${CB}-content`);
            if (id) {
                elements = document.querySelectorAll(`#${CB}-${id}`);
            }
            try {
                elements[elements.length - 1].dispatchEvent(event);
            }
            catch (e) {
                throw new Error('Custombox is not instantiated');
            }
        }
        static closeAll() {
            const event = new CustomEvent(`${CB}:close`);
            const elements = document.querySelectorAll(`.${CB}-content`);
            const t = elements.length;
            for (let i = 0; i < t; i++) {
                elements[i].dispatchEvent(event);
            }
        }
        _close() {
            let close = [
                this.content.bind(CLOSE).then(() => this.content.remove()),
            ];
            if (this.options.overlay.active) {
                close.push(this.overlay
                    .bind(CLOSE)
                    .then(() => {
                    if (this.scroll) {
                        this.scroll.remove();
                    }
                    this.overlay.remove();
                    this.dispatchEvent('overlay.onClose');
                }));
            }
            if (this.container) {
                close.push(this.container
                    .bind(CLOSE)
                    .then(() => this.container.remove()));
            }
            Promise
                .all(close)
                .then(() => {
                if (this.options.content.close) {
                    document.removeEventListener('keydown', this.action, true);
                }
                this.dispatchEvent('content.onClose');
                // Remove lock
                document.body.classList.remove(LOCK);
            });
        }
        // Private methods
        dispatchEvent(type) {
            const element = type.replace('.on', ':').toLowerCase();
            const event = new CustomEvent(`${CB}:${element}`);
            const action = Object.create(this.options);
            document.dispatchEvent(event);
            try {
                type.split('.').reduce((a, b) => a[b], action).call();
            }
            catch (e) { }
        }
        listeners() {
            const AFM = window.getComputedStyle(this.content.element).getPropertyValue('animation-fill-mode');
            document.addEventListener('fullscreenchange', () => {
                const style = window.getComputedStyle(this.content.element);
                if (style.getPropertyValue('animation-fill-mode') === AFM) {
                    this.content.element.style.animationFillMode = 'backwards';
                }
                else {
                    this.content.element.style.animationFillMode = AFM;
                }
            }, true);
            if (this.options.content.close) {
                document.addEventListener('keydown', this.action, true);
            }
            if (this.options.overlay.close) {
                this.content.element.addEventListener('click', (event) => {
                    if (event.target === this.content.element) {
                        this._close();
                    }
                }, true);
            }
            this.content.element.addEventListener(`${CB}:close`, () => {
                this._close();
            }, true);
        }
    }
    Custombox.modal = modal;
})(Custombox || (Custombox = {}));
//# sourceMappingURL=custombox.js.map