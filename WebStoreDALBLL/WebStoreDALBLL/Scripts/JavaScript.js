

var bilde_galleri = new Array();
//oppretter et nytt bildeobjekt i bilde_galleri arrayets indeks 0:
bilde_galleri[0] = new Image();
//setter bildet i bilde_galleri[0]'s kilde:
bilde_galleri[0].src = "https://d1u1p2xjjiahg3.cloudfront.net/30916648-2ea7-494f-8ceb-5aa2bb1b98b6.jpg";
//oppretter et nytt bildeobjekt i bilde_galleri arrayets indeks 1:                                        
bilde_galleri[1] = new Image();
//setter bildet i bilde_galleri[1]'s kilde:
bilde_galleri[1].src = "http://www.smashingmagazine.com/images/pictures-photos/photos-12.jpg";
var i = 0;
var lastPic = bilde_galleri.length - 1;
document.getElementById("flipLeft").onclick = function () {
    if (i > 0)
        i--;
    else
        i = lastPic;

    document.getElementById("mittBilde").src = bilde_galleri[$i].src;
}
document.getElementById("flipRight").onclick = function () {
    window.alert("hei");
    if (i < lastPic)
        i++;
    else
        i = 0;

    document.getElementById("mittBilde").src = bilde_galleri[$i].src;
}