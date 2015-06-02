$(document).ready(function () {
    $('#accountTab a[href="#posts"]').tab('show');
    var currentTab = "";

    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("href");
        var gridManager = TheBlog.GridManager, fn, gridName, pagerName;

        if (target !== currentTab) {
            switch (target) {
                case "#posts":
                    gridManager.postsGrid("#tablePosts", "#pagerPosts");

                    break;
                case "#categories":
                    fn = gridManager.categoriesGrid;
                    gridName = "#tableCategories";
                    pagerName = "#pagerCategories";
                    break;
                case "tags":
                    fn = gridManager.postsGrid;
                    gridName = "#tableTags";
                    pagerName = "#pagertags";
            }
            currentTab = target;
        }
    });
});