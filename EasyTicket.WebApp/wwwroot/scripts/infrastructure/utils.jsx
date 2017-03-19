import React from 'react';
import Error from './error';

export default class Utils extends React.Component {
    generateId() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g,
            c => {
                var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
                return v.toString(16);
            });
    }

    generateErrorHtml(error) {
        return error ? <Error text={`${error.status}: ${error.statusText}`}/> : "";
    }
}