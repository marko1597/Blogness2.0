// This file in the main entry point for defining grunt tasks and using grunt plugins.
// Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409

module.exports = function (grunt) {
    grunt.initConfig({
        bower: {
            install: {
                options: {
                    targetDir: "../wwwroot/lib",
                    layout: "byComponent",
                    cleanTargetDir: false
                }
            }
        },
        ngtemplates: {
            blog: {
                src: ['wwwroot/templates/*.html', 'wwwroot/templates/**/*.html'],
                dest: 'wwwroot/modules/templates.js',
                options: {
                    //htmlmin: {
                    //    collapseBooleanAttributes: true,
                    //    collapseWhitespace: true,
                    //    removeAttributeQuotes: true,
                    //    removeComments: true,
                    //    removeEmptyAttributes: true,
                    //    removeRedundantAttributes: true,
                    //    removeScriptTypeAttributes: true,
                    //    removeStyleLinkTypeAttributes: true
                    //},
                    url: function (url) {
                        url = url.replace('wwwroot/templates/', '');
                        url = url.replace('modules/', '');
                        return url;
                    }
                }
            }
        }
    });

    grunt.registerTask("default", [
        "ngtemplates"
        //, "bower:install"
    ]);

    grunt.loadNpmTasks("grunt-bower-task");
    grunt.loadNpmTasks('grunt-angular-templates');
};