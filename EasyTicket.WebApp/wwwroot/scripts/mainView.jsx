import React from 'react';

export default class MainView extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <p>This is Main</p>
                <p><a href='/#/request'>Go to Request page</a></p>
            </div>
        )
    }
}