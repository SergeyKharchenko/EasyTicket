import React from 'react';

export default class RequestView extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <p>This is Request</p>
                <p><a href='/#/main'>Go to Main page</a></p>
            </div>
        );
    }
}