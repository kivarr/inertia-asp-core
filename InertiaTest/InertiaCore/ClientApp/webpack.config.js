const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CompressionPlugin = require('compression-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const { VueLoaderPlugin } = require('vue-loader');

module.exports = (env = {}, argv = {}) => {

    const isDevBuild = !(env && env.prod);

    const outputDir = (env && env.publishDir)
        ? env.publishDir
        : __dirname;

    const config = {
        mode: isDevBuild ? 'development' : 'production', // we default to development when no 'mode' arg is passed
        devtool: 'source-map',
        optimization: {
            minimize: true
        },
        entry: {
            main: './src/index.ts'
        },
        output: {
            filename: !isDevBuild ? 'bundle-[chunkHash].js' : '[name].js',
            path: path.resolve(outputDir, isDevBuild ? '../wwwroot/dist' : 'wwwroot/dist'),
            publicPath: "/dist/"
        },
        plugins: [
            new MiniCssExtractPlugin({
                filename: !isDevBuild ? 'style-[contenthash].css' : 'style.css'
            }),
            new CompressionPlugin({
                filename: '[path].gz[query]',
                algorithm: 'gzip',
                test: /\.js$|\.css$|\.html$|\.eot?.+$|\.ttf?.+$|\.woff?.+$|\.svg?.+$/,
                threshold: 10240,
                minRatio: 0.8
            }),
            new HtmlWebpackPlugin({
                template: '_Layout.cshtml',
                filename: '../../Views/Shared/_Layout.cshtml', //the output root here is /wwwroot/dist so we ../../      
                inject: false
            }),
            new CleanWebpackPlugin(),
            new VueLoaderPlugin(),
        ],
        module: {
            rules: [
                {
                    test: /\.vue$/,
                    use: ['vue-loader']
                },
                {
                    test: /\.tsx?$/,
                    loader: 'ts-loader',
                    exclude: /node_modules/,
                    options: {
                        appendTsSuffixTo: [/\.vue$/],
                    }
                },
                {
                    test: /\.js$/,
                    exclude: /node_modules/,
                    use: {
                        loader: "babel-loader"
                    }
                },
                {
                    test: /\.css$/,
                    use: [
                        "style-loader",
                        'vue-style-loader',
                        { loader: "css-loader", options: { importLoaders: 1 } },
                        "postcss-loader",
                    ],
                },
                {
                    test: /\.(png|jpg|gif|woff|woff2|eot|ttf|svg)$/,
                    loader: 'file-loader',
                    options: {
                        name: '[name].[hash].[ext]',
                        outputPath: 'assets/'
                    }
                }
            ]
        },
        resolve: {
            extensions: [ '.tsx', '.ts', '.js', '.vue' ],
            alias: {
                '@': path.resolve('./src'),
            }
        },
    };
    return config;
};