var $url = '/database/query';
var $urlExport = '/database/query/actions/export';

var data = utils.init({
  siteId: utils.getQueryInt('siteId'),
  form: {
    query: ''
  },
  results: null,
  properties: null,
  count: 0
});

var methods = {
  apiQuery: function () {
    var $this = this;

    utils.loading($this, true);
    $api.post($url, this.form).then(function (response) {
      var res = response.data;

      $this.results = res.results;
      $this.properties = res.properties;
      $this.count = res.count;
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

  apiExport: function () {
    var $this = this;

    utils.loading($this, true);
    $api.post($urlExport, this.form).then(function (response) {
      var res = response.data;

      window.open(res.value);
    }).catch(function (error) {
      utils.error(error);
    }).then(function () {
      utils.loading($this, false);
    });
  },

  btnSubmitClick: function () {
    var $this = this;
    this.$refs.form.validate(function(valid) {
      if (valid) {
        $this.apiQuery();
      }
    });
  },

  btnExportClick: function () {
    var $this = this;
    this.$refs.form.validate(function(valid) {
      if (valid) {
        $this.apiExport();
      }
    });
  }
};

var $vue = new Vue({
  el: '#main',
  data: data,
  methods: methods,
  created: function () {
    utils.loading(this, false);
  }
});
