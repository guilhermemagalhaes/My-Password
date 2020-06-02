function FecharModal(objModal) {
    objModal.modal('hide');
    $('body').removeClass('modal-open');
    $('body').removeAttr("style");
    $('.modal-backdrop').fadeOut();
}