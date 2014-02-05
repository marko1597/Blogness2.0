window.Blog.DateTime =
{
    getTimeDifferenceInHours: function (datetime) {
        var current = parseInt(new Date().toISOString().split('T')[1].split(':')[0]);
        var item = parseInt(datetime.split('T')[1].split(':')[0]);
        var result = current - item;

        if (result < 0) {
            result = (24 - item) + current;
        }

        return result;
    },

    getTimeDifferenceInMinutes: function (datetime) {
        var current = parseInt(new Date().toISOString().split('T')[1].split(':')[1]);
        var item = parseInt(datetime.split('T')[1].split(':')[1]);
        var result = current - item;

        if (result < 0) {
            result = (60 - item) + current;
        }

        return result;
    },

    generateDateTimeDifference: function (y, m, d, h, mm, s, ms) {
        var dt = y + '-' + m + '-' + d + 'T' + h + ':' + mm + ':' + s + '.' + ms + 'Z';
        var timespan = window.Blog.DateTime.getTimeDifferenceInHours(dt) + ' hours and ' + window.Blog.DateTime.getTimeDifferenceInMinutes(dt) + ' minutes ago';

        return timespan;
    }
}