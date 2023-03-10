//This was helpful for non numeric slider values https://stackoverflow.com/questions/8198267/html-form-input-slider-range-with-non-numeric-values#:~:text=The%20type%20range%20of%20html%20input%20is%20designed,In%20jQuery%2C%20it%20would%20be%20something%20like%20this%3A

var moodSlider = document.getElementById("moodRange");
var moodOutput = document.getElementById("moodValue");
moodOutput.innerHTML = moodSlider.value;
moodOutput.innerHTML = "Happy"
moodSlider.oninput = function () {
    switch ($(this).val()) {
        case "1":
            moodOutput.innerHTML = "Happy"
            break;
        case "2":
            moodOutput.innerHTML = "Sad"
            break;
        case "3":
            moodOutput.innerHTML = "Angry"
            break;
        case "4":
            moodOutput.innerHTML = "Calming"
            break;
        case "5":
            moodOutput.innerHTML = "Energetic"
            break;
        case "6":
            moodOutput.innerHTML = "Dancing"
            break;
    }

}

