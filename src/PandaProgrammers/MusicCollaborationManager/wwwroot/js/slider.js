

var tempoSlider = document.getElementById("tempoRange");
var tempoOutput = document.getElementById("tempoValue");
tempoOutput.innerHTML = tempoSlider.value;
tempoSlider.oninput = function () {
    tempoOutput.innerHTML = this.value;
}


var acoSlider = document.getElementById("acoRange");
var acoOutput = document.getElementById("acoValue");
acoOutput.innerHTML = acoSlider.value;
acoSlider.oninput = function () {
    acoOutput.innerHTML = this.value;
}


var dancSlider = document.getElementById("dancRange");
var dancOutput = document.getElementById("dancValue");
dancOutput.innerHTML = dancSlider.value;
dancSlider.oninput = function () {
    dancOutput.innerHTML = this.value;
}


var engSlider = document.getElementById("engRange");
var engOutput = document.getElementById("engValue");
engOutput.innerHTML = engSlider.value;
engSlider.oninput = function () {
    engOutput.innerHTML = this.value;
}


var instSlider = document.getElementById("instRange");
var instOutput = document.getElementById("instValue");
instOutput.innerHTML = instSlider.value;
instSlider.oninput = function () {
    instOutput.innerHTML = this.value;
}


var liveSlider = document.getElementById("liveRange");
var liveOutput = document.getElementById("liveValue");
liveOutput.innerHTML = liveSlider.value;
liveSlider.oninput = function () {
    liveOutput.innerHTML = this.value;
}


var popSlider = document.getElementById("popRange");
var popOutput = document.getElementById("popValue");
popOutput.innerHTML = popSlider.value;
popSlider.oninput = function () {
    popOutput.innerHTML = this.value;
}


var speechSlider = document.getElementById("speechRange");
var speechOutput = document.getElementById("speechValue");
speechOutput.innerHTML = speechSlider.value;
speechSlider.oninput = function () {
    speechOutput.innerHTML = this.value;
}


var valSlider = document.getElementById("valRange");
var valOutput = document.getElementById("valValue");
valOutput.innerHTML = valSlider.value;
valSlider.oninput = function () {
    valOutput.innerHTML = this.value;
}
