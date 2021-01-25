var currentList = {};

function createShoppingList() {
    currentList.name = $("#shoppinglistName").val();
    currentList.items = new Array();

    //web service call 
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/ShoppingListsEF/" ,
        data: currentList,  // this will return the current list to the service. 
        success: function (result) {
            currentList = result;
            showShoppingList();
            history.pushState({ id: result.id }, result.name, "?id=" + result.id);
        }
    });


}


function showShoppingList() {
    $("#shoppingListTitle").html(currentList.name);
    $("#shoppingListItems").empty();

    $("#createListDiv").hide();
    $("#shoppingListDiv").show();

    $("#newItemName").val("");
    $("#newItemName").focus();
    $("#newItemName").unbind("keyup");
    $("#newItemName").keyup(function (event) {
        if (event.keyCode == 13) {
            addItem();
        }
    });
}

function addItem() {
    var newItem = {};
    newItem.name = $("#newItemName").val();
    newItem.ShoppingListId = currentList.id;

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/ItemsEF/",
        data: newItem,
        success: function (result) {
            currentList = result;
            drawItems();
            $("#newItemName").val("");
        }
    });
    
}

function drawItems() {
    var $list = $("#shoppingListItems").empty();

    for (var i = 0; i < currentList.items.length; i++) {
        var currentItem = currentList.items[i];
        var $li = $("<li>").html(currentItem.name)
            .attr("id", "item_" + i);

        var deleteBtn =
            $("<button onclick='deleteItem(" + currentItem.id + ")' >D</button>").appendTo($li);
        var $checkBtn =
            $("<button onclick='checkItem(" + currentItem.id + ")' >C</button>").appendTo($li);

        if (currentList.checked) {
            $li.addClass("checked");
        }

        $li.appendTo($list);
    }
}


function deleteItem(itemId) {
    $.ajax({
        type: "DELETE",
        dataType: "json",
        url: "api/ItemsEF/" + itemId,
        success: function (result) {
            currentList = result;
            drawItems();
        }
    });
}

function checkItem(itemId) {
    var changedItem = {};

    for (var i = 0; i < currentList.items.length; i++) {
        if (currentList.items[i].id == itemId) {
            changedItem = currentList.items[i];
        }
    }

    changedItem.checked = !changedItem.checked;
    
    $.ajax({
        type: "PUT",
        dataType: "json",
        url: "api/ItemsEF/" + itemId,
        data: changedItem, // this is the data that will be send to the service. 
        success: function (result) {
            changedItem = result;
            drawItems();
        }
    });


}


 function getShoppingListById(id) {
    $.ajax({
        type: "GET", // this is the type of the http request.  
        dataType: "json", // this is the response that we expect from the server. 
        url: "api/ShoppingListsEF/" + id, // the is the url of our call 
        success: function (result) { // what will happne in case of success
            currentList = result;
            showShoppingList();
            drawItems();
        }
    });
}

function hideShoppingList() {
    $("#createListDiv").show();
    $("#shoppingListDiv").hide();


    $("#shoppinglistName").val("");
    $("#shoppinglistName").focus();
    $("#shoppinglistName").unbind("keyup");
    $("#shoppinglistName").keyup(function (event) {
        if (event.keyCode == 13) {
            createShoppingList();
        }
    });

}

$(document).ready(function () {
    console.info("ready");

    hideShoppingList();
    $("#shoppinglistName").focus();
    $("#shoppinglistName").keyup(function (event) {
        if (event.keyCode == 13) {
            createShoppingList();
        }
    });

    var pageUrl = window.location.href;
    var idIndex = pageUrl.indexOf("?id=");

    if (idIndex != -1) {
        getShoppingListById(pageUrl.substring(idIndex + 4));
    }

    window.onpopstate = function (event) {
        if (event.state == null) {
            hideShoppingList();
        }
        else {
            this.getShoppingListById(event.state.id);
        }
    };
});