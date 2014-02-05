window.Blog.Location = 
{
    getUserLocation: function() {
        this.UserLocation = window.geoplugin_countryName() + ", " + window.geoplugin_region() + ", " + window.geoplugin_city();
        return this.UserLocation;
    },

    getUserCountry: function() {
        var country = window.geoplugin_countryName();
        return country;
    },

    getUserRegion: function() {
        var region = window.geoplugin_region();
        return region;
    },

    getUserCity: function() {
        var city = window.geoplugin_city();
        return city;
    }
}