var $url = '/database/execute';

var data = utils.init({
  form: {
    execute: '',
    securityKey: ''
  }
});

var methods = {
  apiExecute: function () {
    var $this = this;

    utils.loading(this, true);
    $api.post($url, this.form).then(function (response) {
      utils.success('SQL命令执行成功！');
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
        $this.apiExecute();
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
