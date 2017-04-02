import Sammy from 'sammy'
import React from 'react';
import Loading from './controls/loading';
import MainView from './views/mainView';
import ReservationView from './views/reservationView';

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
            this.get('#/reservation/:token',
                function () {
                    that.setState({
                        view: 'reservation',
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
            case 'reservation': {
                return <ReservationView token={this.state.data.token}/>;
            }
            default: {
                return <Loading/>;
            }
        }
    }
}