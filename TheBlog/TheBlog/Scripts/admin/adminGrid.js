var TheBlog = {};

TheBlog.GridManager = {

    postsGrid: function (gridName, pagerName) {
        tinyMCE.init({
            // General options
            mode: "textareas",
            theme: "advanced",
            // Theme options
            theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
            theme_advanced_buttons3: "removeformat,visualaid,|,sub,sup,|,charmap",

            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: true

        });

        var afterShowForm = function () {
            tinyMCE.execCommand('mceAddEditor', false, "ShortDescription");
            tinyMCE.execCommand('mceAddEditor', false, "FullDescription");
        };

        var onClose = function () {
            tinyMCE.execCommand('mceRemoveEditor', false, "ShortDescription");
            tinyMCE.execCommand('mceRemoveEditor', false, "FullDescription");
        };

        var columns = [];
        columns.push({
            name: 'PostId',
            hidden: true,
            editable: true,
            key: true
        });

        columns.push({
            name: 'Title',
            index: 'Title',
            width: 250,
            editable: true,
            editoptions: {
                size: 43,
                maxlength: 300
            },
            editrules: {
                required: true
            }
        });

        columns.push({
            name: 'ShortDescription',
            index: 'ShortDescription',
            width: 250,
            sortable: false,
            hidden: true,
            editable: true,
            edittype: 'textarea',
            editoptions: {
                rows: "10",
                cols: "100"
            },
            editrules: {
                custom: true,
                custom_func: function (val, colname) {
                    val = tinyMCE.get("ShortDescription").getContent();
                    if (val) return [true, ""];
                    return [false, colname + ": Field is required"];
                },
                edithidden: true
            }
        });

        columns.push({
            name: 'FullDescription',
            index: 'FullDescription',
            sortable: false,
            width: 250,
            hidden: true,
            editable: true,
            edittype: 'textarea',
            editoptions: {
                rows: "40",
                cols: "100"
            },
            editrules: {
                custom: true,

                custom_func: function (val, colname) {
                    val = tinyMCE.get("FullDescription").getContent();
                    if (val) return [true, ""];
                    return [false, colname + ": Field is requred"];
                },
                edithidden: true
            }
        });

        columns.push({
            name: 'CategoryId',
            hidden: true,
            editable: true,
            edittype: 'select',
            editoptions: {
                style: 'width:250px;',
                dataUrl: '/root/GetCategoriesHtml'
            },
            editrules: {
                required: true,
                edithidden: true
            }
        });

        columns.push({
            name: 'Category.Name',
            label: 'Category'
        });

        columns.push({
            name: 'Tags',
            width: 150,
            editable: true,
            edittype: 'select',
            editoptions: {
                style: 'width:250px;',
                dataUrl: '/root/GetTagsHtml',
                multiple: true
            },
            editrules: {
                required: true
            }
        });

        columns.push({
            name: 'AddedOn',
            index: 'AddedOn',
            align: 'center',
            sorttype: 'date',
            datefmt: 'm/d/Y'
        });

        columns.push({
            name: 'User.Username',
            label: 'Username'
        });

        columns.push({
            name: 'IsPublished',
            index: 'IsPublished',
            width: 100,
            align: 'center',
            editable: true,
            edittype: 'checkbox',
            editoptions: {
                value: "true:false",
                defaultValue: 'false'
            }
        });

        $(gridName).jqGrid({
            url: '/root/Posts',
            datatype: 'json',
            mtype: 'GET',
            pager: pagerName,
            colModel: columns,

            width: 850,
            rowNum: 10,
            rowList: [10, 20, 30],

            // row number column
            rownumbers: true,
            rownumWidth: 40,

            // default sorting
            sortname: 'AddedOn',
            sortorder: 'desc',

            // display the no. of records message
            viewrecords: true,

            jsonReader: { repeatitems: false },

            afterInsertRow: function (rowid, rowdata, rowelem) {
                var tags = rowdata["Tags"];
                var tagStr = "";

                $.each(tags, function (i, t) {
                    if (tagStr) tagStr += ", ";
                    tagStr += t.Name;
                });

                $(gridName).setRowData(rowid, { "Tags": tagStr });
            }
        });

        var beforeSubmitHandler = function (postdata) {
            var selRowData = $(gridName).getRowData($(gridName).getGridParam('selrow'));
            if (selRowData["AddedOn"])
                postdata.PostedOn = selRowData["AddedOn"];
            postdata.ShortDescription = tinyMCE.get("ShortDescription").getContent();
            postdata.FullDescription = tinyMCE.get("FullDescription").getContent();

            return [true];
        };

        var addOptions = {
            url: '/root/AddPost',
            addCaption: 'Add Post',
            processData: "Saving...",
            width: 900,
            closeAfterAdd: true,
            closeOnEscape: true,
            afterShowForm: afterShowForm,
            onClose: onClose,
            afterSubmit: TheBlog.GridManager.afterSubmitHandler,
            beforeSubmit: beforeSubmitHandler
        };

        var editOptions = {
            url: '/root/EditPost',
            addCaption: 'Edit Post',
            processData: "Saving...",
            width: 900,
            closeAfterEdit: true,
            closeOnEscape: true,
            afterShowForm: afterShowForm,
            onClose: onClose,
            afterSubmit: TheBlog.GridManager.afterSubmitHandler,
            beforeSubmit: beforeSubmitHandler
        };

        var deleteOptions = {
            url: '/root/DeletePost',
            caption: 'Delete Post',
            processData: "Deleting...",
            msg: "Are you sure?",
            closeOnEscape: true,
            afterSubmit: TheBlog.GridManager.afterSubmitHandler
        };

        $(gridName).navGrid(pagerName,
                                    {
                                        cloneToTop: true,
                                        search: false
                                    },
                                    editOptions,
                                    addOptions,
                                    deleteOptions
        );

    },

    afterSubmitHandler: function (response, postdata) {

        var json = $.parseJSON(response.responseText);

        if (json) return [json.success, json.message, json.id];

        return [false, "Failed to get result from server.", null];
    },

    categoriesGrid: function (gridName, pagerName) {
    },

    tagsGrid: function (gridName, pagerName) {
    }
}