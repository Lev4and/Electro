/**
 * Actions for product.
 *
 * @author Lev4and
 * @version 1.0
 *
*/
initializationProductItems();

function initializationProductItems() {
    setEventListenerForProductItems();
    setEventListenerForLinkCartAction();
    setEventListenerForLinkCompareAction();
}

function setEventListenerForProductItems() {
    $('a[data-product-action]').each(function () {
        $(this).on('click', function (e) {
            switch ($(this).attr('data-product-action')) {
                case 'favorite':
                    onClickFavorite(this);
                    break;
                case 'compare':
                    onClickCompare(this);
                    break;
                case 'cart':
                    onClickCart(this);
                    break;
                default:
                    break;
            }
        });
    });
}

function setEventListenerForLinkCartAction() {
    $('a[data-cart-action]').each(function () {
        $(this).on('click', function (e) {
            switch ($(this).attr('data-cart-action')) {
                case 'remove':
                    onClickRemoveProductFromCart(this);
                    break;
                default:
                    break;
            }
        });
    });
}

function setEventListenerForLinkCompareAction() {
    $('a[data-compare-action]').each(function () {
        $(this).on('click', function (e) {
            switch ($(this).attr('data-compare-action')) {
                case 'remove':
                    onClickRemoveProductFromCompare(this);
                    break;
                default:
                    break;
            }
        });
    });
}

function onClickFavorite(sender) {
    let productId = $(sender).attr('data-product-id');
    let state = $(sender).attr('data-product-favorite-state');

    if (state === "true") {
        $.ajax({
            url: '/Favorites/Remove',
            method: 'POST',
            data: { productId: productId },
            success: function (data) {
                $('a[data-product-action="favorite"][data-product-id="' + productId + '"]').each(function () {
                    $(this).attr('data-product-favorite-state', false);
                });

                $('a[data-original-title="Список желаний"]').children('span').html(data.count);
            }
        });
    }
    else {
        $.ajax({
            url: '/Favorites/Add',
            method: 'POST',
            data: { productId: productId },
            success: function (data) {
                $('a[data-product-action="favorite"][data-product-id="' + productId + '"]').each(function () {
                    $(this).attr('data-product-favorite-state', true);
                });

                $('a[data-original-title="Список желаний"]').children('span').html(data.count);
            }
        });
    }
}

function onClickCompare(sender) {
    let productId = $(sender).attr('data-product-id');
    let categoryId = $(sender).attr('data-product-category-id');

    let state = $(sender).attr('data-product-compare-state');

    if (state === "true") {
        $.ajax({
            url: '/Compare/Remove',
            method: 'POST',
            data: { categoryId: categoryId, productId: productId },
            success: function (data) {
                $('a[data-product-action="compare"][data-product-id="' + productId + '"]').each(function () {
                    $(this).attr('data-product-compare-state', false);
                });

                $('a[data-original-title="Сравнить"]').children('span').html(data.count);
                $('div[data-original-title="Сравнить"]').children('span').html(data.count);

                updateContentCompare();

                if ($(this).attr('data-compare-action') === "update") {
                    window.location.reload();
                }
            }
        });
    }
    else {
        $.ajax({
            url: '/Compare/Add',
            method: 'POST',
            data: { categoryId: categoryId, productId: productId },
            success: function (data) {
                $('a[data-product-action="compare"][data-product-id="' + productId + '"]').each(function () {
                    $(this).attr('data-product-compare-state', true);
                });

                $('a[data-product-action="compare"][data-product-category-id!="' + categoryId + '"]').each(function () {
                    $(this).attr('data-product-compare-state', false);
                });

                $('a[data-original-title="Сравнить"]').children('span').html(data.count);
                $('div[data-original-title="Сравнить"]').children('span').html(data.count);

                updateContentCompare();
            }
        });
    }
}

function onClickCart(sender) {
    let productId = $(sender).attr('data-product-id');
    let quantity = $('input[name="quantity"]').val();

    quantity = quantity != undefined ? quantity : 1;

    $.ajax({
        url: '/Cart/Add',
        method: 'POST',
        data: { productId: productId, quantity: quantity },
        success: function (data) {
            $('a[data-original-title="Корзина"]').children('span').html(data.count);
            $('div[data-original-title="Корзина"]').children('span').html(data.count);

            updateContentCart();
        }
    });
}

function onClickRemoveProductFromCart(sender) {
    let productId = $(sender).attr('data-cart-product-id');

    $.ajax({
        url: '/Cart/Remove',
        method: 'POST',
        data: { productId: productId },
        success: function (data) {
            $('a[data-original-title="Корзина"]').children('span').html(data.count);
            $('div[data-original-title="Корзина"]').children('span').html(data.count);

            updateContentCart();
        }
    });
}

function onClickRemoveProductFromCompare(sender) {
    let productId = $(sender).attr('data-compare-product-id');
    let categoryId = $(sender).attr('data-compare-category-id');

    $.ajax({
        url: '/Compare/Remove',
        method: 'POST',
        data: { categoryId: categoryId, productId: productId },
        success: function (data) {
            $('a[data-product-action="compare"][data-product-id="' + productId + '"]').each(function () {
                $(this).attr('data-product-compare-state', false);
            });

            $('a[data-original-title="Сравнить"]').children('span').html(data.count);
            $('div[data-original-title="Сравнить"]').children('span').html(data.count);

            updateContentCompare();
        }
    });
}

function updateContentCart() {
    $.ajax({
        url: '/Cart/DropdownContent',
        method: 'POST',
        success: function (data) {
            $('#cartDropdownHover').html();
            $('#cartDropdownHover').html(data);

            setEventListenerForLinkCartAction();
        }
    });
}

function updateContentCompare() {
    $.ajax({
        url: '/Compare/DropdownContent',
        method: 'POST',
        success: function (data) {
            $('#compareDropdownHover').html();
            $('#compareDropdownHover').html(data);

            setEventListenerForLinkCompareAction();
        }
    });
}