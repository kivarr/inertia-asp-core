module.exports = {
    plugins: [
        require('postcss-import'),
        require("tailwindcss")("./tailwind.config.js"),
        require('postcss-nesting'),
        require('postcss-custom-properties'),
        require("autoprefixer"),
    ],
};