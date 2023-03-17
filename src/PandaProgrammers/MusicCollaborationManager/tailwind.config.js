module.exports = {
    mode: 'jit',
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.cshtml',
        './Areas/**/*.csthml',
        './wwwroot/**/*.js'
    ],
    theme: {
        extend: {},
    },
    plugins: [
        require('tailwindcss-themer')({
            defaultTheme: {
              // put the default values of any config you want themed
              // just as if you were to extend tailwind's theme like normal https://tailwindcss.com/docs/theme#extending-the-default-theme
              extend: {
                // colors is used here for demonstration purposes
                colors: {
                  primary: 'red'
                }
              }
            },
            themes: [
              {
                                    // name your theme anything that could be a valid css selector
                    // remember what you named your theme because you will use it as a class to enable the theme
                    name: 'revolution',
                    // put any overrides your theme has here
                    // just as if you were to extend tailwind's theme like normal https://tailwindcss.com/docs/theme#extending-the-default-theme
                    extend: {
                        'colors': {
                            coreback: '#840B07',
                            primary: 'yellow',
                            secondaryback: '#6F6F6F',
                            primback: '#d1d5db',
                            textback: '#000000'
                        }

                    }
                   
                },
                {
                    // name your theme anything that could be a valid css selector
                    // remember what you named your theme because you will use it as a class to enable the theme
                    name: 'classicpanda',
                    // put any overrides your theme has here
                    // just as if you were to extend tailwind's theme like normal https://tailwindcss.com/docs/theme#extending-the-default-theme
                    extend: {
                        'colors': {
                            coreback: '#1F2937',
                            'primary': 'gold',
                            secondaryback: '#D1D5DB',
                            primback: '#d1d5db',
                            textback: '#000000',
                            whitetext: '#ffffff'
                        }

                    }

                },
                {
                    // name your theme anything that could be a valid css selector
                    // remember what you named your theme because you will use it as a class to enable the theme
                    name: 'autumn',
                    // put any overrides your theme has here
                    // just as if you were to extend tailwind's theme like normal https://tailwindcss.com/docs/theme#extending-the-default-theme
                    extend: {
                        'colors': {
                             coreback: '#800080',
                            'primary': 'purple',
                            secondaryback: '#A9A9A9',
                            primback: '#d1d5db',
                            textback: '#000000'
                        }

                    }

                },
                {
                    // name your theme anything that could be a valid css selector
                    // remember what you named your theme because you will use it as a class to enable the theme
                    name: 'moon',
                    // put any overrides your theme has here
                    // just as if you were to extend tailwind's theme like normal https://tailwindcss.com/docs/theme#extending-the-default-theme
                    extend: {
                        'colors': {
                            coreback: '#1f2937',
                            'primary': 'black',
                            secondaryback: '#1f2937',
                            primback: '#0f172a',
                            textback: '#ffffff'
                        }

                    }

                },
                {
                    // name your theme anything that could be a valid css selector
                    // remember what you named your theme because you will use it as a class to enable the theme
                    name: 'luxury',
                    // put any overrides your theme has here
                    // just as if you were to extend tailwind's theme like normal https://tailwindcss.com/docs/theme#extending-the-default-theme
                    extend: {
                        'colors': {
                            coreback: '#000000',//Navbar
                            'primary': 'black',
                            secondaryback: '#000000',//Container
                            primback: '#C0C0C0', //HTML Background
                            textback: '#ffffff'
                        }

                    }

                }
            ]
          })
  ]
}
