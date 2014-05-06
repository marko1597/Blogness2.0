ngShared.factory('dateHelper', [function () {
    return {
        getJsFullDate: function(jsonDate) {
            return new Date(jsonDate);
        },

        getMonthName: function(month) {
            var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            return months[month];
        },

        getJsDate: function(jsonDate) {
            var itemDate = new Date(jsonDate);
            var day = itemDate.getDate();
            var month = this.getMonthName(itemDate.getMonth());
            var year = itemDate.getFullYear();

            return month + " " + day + ", " + year;
        },

        getJsTime: function(jsonDate) {
            var itemDate = new Date(jsonDate);
            var hour = itemDate.getHours();
            var minutes = itemDate.getMinutes();

            return hour + ":" + minutes;
        },

        getDateDisplay: function(jsonDate) {
            var itemDate = new Date(jsonDate);
            var currDate = new Date();
            var diff = (parseInt(currDate - itemDate) / 1000 / 60 / 60).toFixed(2);

            if (diff < 24) {
                return Math.round(diff) + " hours ago " + this.getJsTime(jsonDate);
            } else if (diff < 48) {
                return "A day ago " + this.getJsTime(jsonDate);
            } else if (diff < 48) {
                return "2 days ago " + this.getJsTime(jsonDate);
            } else if (diff < 72) {
                return "3 day ago " + this.getJsTime(jsonDate);
            } else if (diff < 96) {
                return "4 day ago " + this.getJsTime(jsonDate);
            } else if (diff < 120) {
                return "5 day ago " + this.getJsTime(jsonDate);
            } else if (diff < 144) {
                return "6 day ago " + this.getJsTime(jsonDate);
            } else {
                return this.getJsDate(jsonDate) + " " + this.getJsTime(jsonDate);
            }
        }
    };
}]);