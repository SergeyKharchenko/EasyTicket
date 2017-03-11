import Sammy from 'sammy'
import React from 'react';
import LoadingView from './loadingView';
import MainView from './mainView';
import RequestView from './requestView';

export default class Application extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            view: null
        }
    }

    componentDidMount() {
        var that = this;
        new Sammy(function() {
            this.get('#/main',
                function() {
                    that.setState({view: 'main'});
                });
            this.get('#/request',
                function() {
                    that.setState({view: 'request'});
                });
            this.get(/.*/,
                function() {
                    this.redirect('#/main');
                });
        }).run();
    }

    render() {
        switch(this.state.view) {
            case 'main': {
                return <MainView/>;
            }        
            case 'request': {
                return <RequestView/>;
            }
            default: {
                return <LoadingView/>;
            }
        }
    }
}