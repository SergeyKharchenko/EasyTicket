import 'materialize-css';

export default class Toast {
    constructor(text) {
        this.text = text;
    }

    show() {
        Materialize.toast(this.text, 40000);
    }
}