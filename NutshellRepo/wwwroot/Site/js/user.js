function clickAll()
{

    if ($('.toDeleteCheckbox_biger:first').prop('checked')) {

        $('.toDeleteCheckbox').prop('checked', true);
    }
    else
    {
        $('.toDeleteCheckbox').prop('checked', false);
    }

}