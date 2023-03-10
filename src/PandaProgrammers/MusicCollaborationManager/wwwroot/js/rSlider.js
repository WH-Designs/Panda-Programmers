//This was helpful for non numeric slider values https://stackoverflow.com/questions/8198267/html-form-input-slider-range-with-non-numeric-values#:~:text=The%20type%20range%20of%20html%20input%20is%20designed,In%20jQuery%2C%20it%20would%20be%20something%20like%20this%3A

var moodSlider = document.getElementById("moodRange");
var moodOutput = document.getElementById("moodValue");
var moodDesc = document.getElementById("moodDescription")
var moodGenres = document.getElementById("moodGenres")
moodOutput.innerHTML = moodSlider.value;
moodOutput.innerHTML = "Happy"
moodDesc.innerHTML = "Happy mood will generally be more cheeful and upbeat. These may be songs you may want to listen to when you are in a good mood."
moodGenres.innerHTML = "Acoustic, Reggae, Pop, Alt-Rock, Guitar, Summer, Groove, Happy, World-Music"
moodSlider.oninput = function () {
    switch ($(this).val()) {
        case "1":
            moodOutput.innerHTML = "Happy"
            moodDesc.innerHTML = "Happy mood will generally be more cheeful and upbeat. These may be songs you may want to listen to when you are in a good mood."
            moodGenres.innerHTML = "Acoustic, Reggae, Pop, Alt-Rock, Guitar, Summer, Groove, Happy, World-Music"
            break;
        case "2":
            moodOutput.innerHTML = "Sad"
            moodDesc.innerHTML = "Sad music will generally be more gloomy. This may come in the form of the lyrics or the overall feel of a song."
            moodGenres.innerHTML = "Country, Sad, Blues, Acoustic, Emo, Bluegrass, Goth, Opera"
            break;
        case "3":
            moodOutput.innerHTML = "Angry"
            moodDesc.innerHTML = "Angry music will generally be intense, fast paced, and potentially dark. You may want to listen to these songs while working out or doing something fast paced."
            moodGenres.innerHTML = "Death-Metal, Emo, Hardcore, Punk-Rock, Heavy-Metal, Alt-Rock, Black-Metal, Grunge, Indie, Metalcore, Punk-Rock"
            break;
        case "4":
            moodOutput.innerHTML = "Chill"
            moodDesc.innerHTML = "Chill music will generally be songs with less energy and a slower tempo. These songs may make good background music or you may want to listen to them when relaxing."
            moodGenres.innerHTML = "Classical, chill, jazz, ambient, study, piano, guitar"
            break;
        case "5":
            moodOutput.innerHTML = "Energetic"
            moodDesc.innerHTML = "Energetic music will generally be more intense and upbeat. You may want to listen to these songs while working out or when you need some motivation through music."
            moodGenres.innerHTML = "Work-Out, Rock-n-Roll, Pop, Hip-Hop, Metal, Brazil, Anime, Dubstep, Electronic, Heavy-Metal, Techno"
            break;
        case "6":
            moodOutput.innerHTML = "Dancing"
            moodDesc.innerHTML = "Dancing music will generally be upbeat and have dancebility. You may want to dance to these songs."
            moodGenres.innerHTML = "Salsa, Tango, Dance, Disco, Hip-Hop, Dancehall, Club, Party, Reggaeton"
            break;
    }

}

