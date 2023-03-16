const addValue = (id, max) => {
    //console.log('test')
    var effect = document.getElementById('qty-' + id);
    var qty = effect.value;

    if (max > qty) {

        if (!isNaN(qty)) {
            //console.log(qty + "before")
            qty++
            //console.log(qty + "after")
            effect.value = qty
            console.log(effect)
            return;
        }
    }
    return false;
}

const subtractValue = (id) => {
    //console.log('test')
    var effect = document.getElementById('qty-' + id);
    var qty = effect.value;

    if (!isNaN(qty) && qty > 1) {
        //console.log(qty + " before")
        qty--
        //console.log(qty + " after")
        effect.value = qty
        console.log(effect)
        return;
    }
    return false;
}
