// styles
import './styles/site'
//import './css/1'

// scripts
import React from 'react';
import ReactDOM from 'react-dom';
import Utils from './scripts/infrastructure/utils';
import Application from './scripts/application';

// start
$(function() {
    $.extend(window.easyTicket, { utils: new Utils() });

    ReactDOM.render(<Application />, document.getElementById('container'));
});