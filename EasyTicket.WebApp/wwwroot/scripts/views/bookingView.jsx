import React from 'react';
import 'jquery.cookie'; 

export default class BookingView extends React.Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        $.ajax({
            url: 'http://booking.uz.gov.ua/ru/',
            type: 'GET',
            crossDomain: true,
            success: function (res) {
                debugger;
                var headline = $(res.responseText).find('a.tsh').text();
                alert(headline);
            }
        });
        

    }


    render() {
        return (
            <div>
                <p>This is Booking</p>
                <p>{this.props.token}</p>
                <p><a href='/#/main'>Go to Main page</a></p>
                <div id="a"></div>
            </div>
        );
    }
}