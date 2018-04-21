//$(document).ready(function () {
//    debugger
//    $(document).on('change', '.btn-file :file', function () {
//        var input = $(this),
//            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
//        input.trigger('fileselect', [label]);
//    });

//    $('.btn-file :file').on('fileselect', function (event, label) {
//        debugger

//        var input = $(this).parents('.input-group').find(':text'),
//            log = label;

//        if (input.length) {
//            input.val(log);
//        } else {
//            if (log) alert(log);
//        }

//    });
//    function readURL(input) {
//        if (input.files && input.files[0]) {
//            var reader = new FileReader();

//            reader.onload = function (e) {
//                $("#img-upload").attr('src', e.target.result);
//            }

//            reader.readAsDataURL(input.files[0]);
//        }
//    }

//    $("#imgInp").change(function () {
//        debugger
//        readURL(this);
//    });
//});

////function showModalWin() {

////            var darkLayer = document.createElement('div'); // слой затемнения
////            darkLayer.id = 'shadow'; // id чтобы подхватить стиль
////            document.body.appendChild(darkLayer); // включаем затемнение

////            var modalWin = document.getElementById('popupWin'); // находим наше "окно"
////            modalWin.style.display = 'block'; // "включаем" его

////            darkLayer.onclick = function() { // при клике на слой затемнения все исчезнет
////                darkLayer.parentNode.removeChild(darkLayer); // удаляем затемнение
////                modalWin.style.display = 'none'; // делаем окно невидимым
////                return false;
////            };
////        }