ngDateHelper.factory('dateHelper', [function () {
    return {
        getJsFullDate: function (jsonDate) {
            return moment(jsonDate);
        },

        getYearsDifference: function (jsonDate) {
            return moment().diff(jsonDate, 'years');
        },
        
        getJsDate: function (jsonDate) {
            var date = moment(jsonDate).format("MMM D, YYYY");
            return date;
        },

        getMonthYear: function(jsonDate) {
            var date = moment(jsonDate).format("MMMM YYYY");
            return date;
        },

        getJsTime: function (jsonDate) {
            var time = moment(jsonDate).format("hh:mm A");
            return time;
        },

        getDateDisplay: function (jsonDate) {
            var itemDate = moment(jsonDate);
            var currDate = moment();
            
            return itemDate.from(currDate) + " at " + this.getJsTime(jsonDate);
        }
    };
}]);