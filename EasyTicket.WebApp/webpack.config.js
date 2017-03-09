var path = require('path');
var webpack = require("webpack");

var rootPath = "wwwroot";
var buildFolder = "build";

module.exports = {
    context: path.resolve(rootPath),
    entry: ["./entryPoint"],
    output: {
        path: path.resolve(path.join(rootPath, buildFolder)),
        publicPath: buildFolder + '/',
        filename: "bundle.js"
    },
    module: {
        loaders: [
            {
                test: /\.jsx?$/,
                exclude: /node_modules/,
                loader: "babel-loader",
                query: {
                    presets: ['es2015', 'react']
                }
            },
            {
                test: /\.css?$/,
                exclude: /node_modules/,
                loader: "style-loader!css-loader"
            },
            {
                test: /\.scss?$/,
                exclude: /node_modules/,
                loader: "style-loader!css-loader!sass-loader"
            },
            {
                test: /\.(png|jpg|jpeg|gif|svg|woff|woff2|eot|ttf)?$/,
                exclude: /node_modules/,
                loader: "url-loader?limit=10000"
            }
        ]
    },
    resolve: {
        extensions: ['.js', '.jsx', '.css', '.scss'],
        alias: {
            'jquery': path.resolve('node_modules/jquery/dist/jquery'),
            'materialize-scss': path.resolve('node_modules/materialize-css/sass/materialize.scss')
        }
    },
    plugins: [
        new webpack.ProvidePlugin({
            $: 'jquery',
            jQuery: "jquery",
            'window.$': 'jquery',
            'window.jQuery': 'jquery'
        })
    ],
    devtool: 'source-map'
};