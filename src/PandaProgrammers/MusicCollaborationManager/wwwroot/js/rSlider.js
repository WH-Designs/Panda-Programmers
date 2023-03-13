//This was helpful for non numeric slider values https://stackoverflow.com/questions/8198267/html-form-input-slider-range-with-non-numeric-values#:~:text=The%20type%20range%20of%20html%20input%20is%20designed,In%20jQuery%2C%20it%20would%20be%20something%20like%20this%3A

var moodSlider = document.getElementById("moodRange");
var moodOutput = document.getElementById("moodValue");
var moodDesc = document.getElementById("moodDescription")
var moodGenres = document.getElementById("moodGenres")
moodOutput.innerHTML = moodSlider.value;
moodOutput.innerHTML = "Happy"
moodDesc.innerHTML = "Happy mood will generally be more cheeful and upbeat. These may be songs you may want to listen to when you are in a good mood."
moodSlider.oninput = function () {
    switch ($(this).val()) {
        case "1":
            moodOutput.innerHTML = "Happy"
            moodDesc.innerHTML = "Happy mood will generally be more cheeful and upbeat. These may be songs you may want to listen to when you are in a good mood."
            break;
        case "2":
            moodOutput.innerHTML = "Sad"
            moodDesc.innerHTML = "Sad music will generally be more gloomy or have sad lyrics. This may come in the form of the lyrics or the overall feel of a song."
            break;
        case "3":
            moodOutput.innerHTML = "Angry"
            moodDesc.innerHTML = "Angry music will generally be intense and potentially dark. You may want to listen to these songs while working out or doing something fast paced."
            break;
        case "4":
            moodOutput.innerHTML = "Chill"
            moodDesc.innerHTML = "Chill music will generally be songs with less energy and a slower tempo. These songs may make good background music or you may want to listen to them when relaxing."
            break;
        case "5":
            moodOutput.innerHTML = "Energetic"
            moodDesc.innerHTML = "Energetic music will generally be more intense and upbeat. You may want to listen to these songs while working out or when you need some motivation through music."
            break;
        case "6":
            moodOutput.innerHTML = "Dancing"
            moodDesc.innerHTML = "Dancing music will generally be upbeat and have dancebility. You may want to dance to these songs."
            break;
    }

}

