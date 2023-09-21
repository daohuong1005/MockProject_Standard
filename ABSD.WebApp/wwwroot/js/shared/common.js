const common = {
    configs: {
        keycodes: [8, 13, 37, 39, 46, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57],
        keys: {
            BackSpace: 8,
            Delete: 46,
            Enter: 13
        }
    },
    getFilterName: (type) => {
        switch (type) {
            case 1:
                return "0123456789";
            case 2:
                return "ABCDE";
            case 3:
                return "FGHIJ";
            case 4:
                return "KLMN";
            case 5:
                return "OPQR";
            case 6:
                return "STUV";
            case 7:
                return "WXYZ";
            default:
                return "";
        }
    },
    notify: (message, type) => {
        $.notify(message, {
            clickToHide: true,
            autoHide: true,
            autoHideDelay: 5000,
            arrowShow: true,
            arrowSize: 5,
            position: '...',
            elementPosition: 'top right',
            globalPosition: 'top right',
            style: 'bootstrap',
            className: type,
            showAnimation: 'slideDown',
            showDuration: 400,
            hideAnimation: 'slideUp',
            hideDuration: 200,
            gap: 2
        });
    },

    confirm: (message, okCallBack) => {
        bootbox.confirm({
            message: message,
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'custom-btn'
                },
                cancel: {
                    label: 'No',
                    className: 'custom-btn'
                }
            },
            callback: function (result) {
                if (result === true) {
                    okCallBack();
                }
            }
        });
    },

    dateFormatJson: (date) => {
        if (!date)
            return '';

        const newDate = new Date(parseInt(date.substr(6)));

        let month = newDate.getMonth() + 1;
        let day = newDate.getDate();
        let year = newDate.getFullYear();

        if (month < 10) month = "0" + month;
        if (day < 10) day = "0" + day;

        return day + "/" + month + "/" + year;
    },

    dateTimeFormatJson: (date) => {
        if (!date)
            return '';

        const newDate = new Date(parseInt(date.substr(6)));

        let month = newDate.getMonth() + 1;
        let day = newDate.getDate();
        let year = newDate.getFullYear();
        let hh = newDate.getHours();
        let mm = newDate.getMinutes();
        let ss = newDate.getSeconds();

        if (month < 10) month = "0" + month;
        if (day < 10) day = "0" + day;
        if (hh < 10) hh = "0" + hh;
        if (mm < 10) mm = "0" + mm;
        if (ss < 10) ss = "0" + ss;

        return day + "/" + month + "/" + year + " " + hh + ":" + mm + ":" + ss;
    },

    startLoading: (target) => {
        //if ($('.div-loading').length > 0)
        //    $('.div-loading').removeClass('hide');
        if ($('.div-loading').length == 0)
            $(target).appendTo('<div class="div-loading">'
                + '<div class= "spinner-border text-primary" role = "status" > <span class="sr-only">Loading...</span></div>'
                + '</div>');
    },

    stopLoading: () => {
        if ($(target).find('.div-loading').length > 0)
            $(target).find('.div-loading').remove();
    },

    formatNumber: (number, precision) => {
        if (!isFinite(number))
            return number.toString();

        const a = number.toFixed(precision).split('.');

        a[0] = a[0].replace('/\d(?=(\d{3})+$)/g', '$&,');

        return a.join('.');
    },

    unflattern: (array) => {
        let map = {};
        let roots = [];

        for (let i = 0; i < array.length; i++) {
            let node = array[i];

            node.children = [];
            map[node.Id] = i;

            if (node.ParentId !== null)
                array[map[node.ParentId]].children.push(node);
            else
                roots.push(node);
        }

        return roots;
    }
}

$(document).ajaxSend((e, xhr, options) => {
    if (options.type.toUpperCase() === "POST") {
        var antiForeignToken = $('form').find('input[name="__RequestVerificationToken"]').val();
        xhr.setRequestHeader("RequestVerificationToken", antiForeignToken);
    }
})