$(() => {
    $('#add-rows').on('click', AddRows);

    console.log("hi");
})

var num = 1;

function AddRows() {
    console.log("testing");
    $("#ppl-rows").append(`<div class="row person-row" style="margin-bottom: 10px;">
        <div class= "col-md-4">
        <input class="form-control" type="text" name="people[${num}].firstname" placeholder="First Name">
        </div>
        <div class="col-md-4">
        <input class="form-control" type="text" name="people[${num}].lastname" placeholder="Last Name">
        </div>
        <div class="col-md-4">
        <input class="form-control" type="text" name="people[${num}].age" placeholder="Age"> </div>
        </div >`);
    num++;
}
