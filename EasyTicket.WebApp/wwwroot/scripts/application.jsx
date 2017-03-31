import Sammy from 'sammy'
import React from 'react';
import LoadingView from './views/loadingView';
import MainView from './views/mainView';
import BookingView from './views/bookingView';

export default class Application extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            view: null,
            data: null
        }
    }

    componentDidMount() {
        var that = this;
        new Sammy(function() {
            this.get('#/main',
                function() {
                    that.setState({view: 'main'});
                });
            this.get('#/booking/:token',
                function () {
                    that.setState({
                        view: 'request',
                        data: {
                            token: this.params['token']
                        }
                    });
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
                return <BookingView token={this.state.data.token}/>;
            }
            default: {
                return <LoadingView/>;
            }
        }
    }
}