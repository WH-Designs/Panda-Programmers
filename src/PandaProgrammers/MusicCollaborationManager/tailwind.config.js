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
                    name: 'revolution',
                    extend: {
                        'colors': {
                            coreback: '#840B07',
                            primary: 'yellow',
                            secondaryback: '#6F6F6F',
                            primback: '#d1d5db',
                            textback: '#000000'
                        },
                        fontFamily:{
                            'header': ['Bangers','cursive', 'Arial', 'sans-serif'],
                            'child': ['"Carter One"', 'system-ui','Helvetica', 'sans-serif']
                        }
                    }
                },
                {
                    name: 'classicpanda',  //classicpanda (default theme)
                    extend: {
                        'colors': {
                            coreback: '#1F2937',
                            'primary': 'gold',
                            secondaryback: '#D1D5DB',
                            primback: '#d1d5db',
                            textback: '#000000',
                            whitetext: '#ffffff'
                        },
                        fontFamily:{
                            'header': ['Vidaloka','cursive', 'sans-serif'],
                            'child': ['Montserrat', 'Helvetica', 'Arial', 'sans-serif']
                        }
                    }
                },
                {
                    name: 'autumn', //Purple
                    extend: {
                        'colors': {
                             coreback: '#800080',
                            'primary': 'purple',
                            secondaryback: '#A9A9A9',
                            primback: '#d1d5db',
                            textback: '#000000'
                        },
                        fontFamily:{
                            'header': ['Gelasio','cursive', 'system-ui', 'sans-serif'],
                            'child': ['Abel', 'Helvetica', 'Arial', 'sans-serif']
                        }
                    }
                },
                {
                    name: 'moon', //Dark
                    extend: {
                        'colors': {
                            coreback: '#1f2937',
                            'primary': 'black',
                            secondaryback: '#1f2937',
                            primback: '#0f172a',
                            textback: '#ffffff'
                        },
                        fontFamily:{
                            'header': ['Vidaloka','cursive', 'sans-serif'],
                            'child': ['Montserrat', 'Helvetica', 'Arial', 'sans-serif']
                        }
                    }
                },
                {
                    name: 'luxury', //Gold
                    extend: {
                        'colors': {
                            coreback: '#000000',//Navbar
                            'primary': 'black',
                            secondaryback: '#000000',//Container
                            primback: '#C0C0C0', //HTML Background
                            textback: '#ffffff'
                        },
                        fontFamily:{
                            'header': ['"roboto condensed"','Arial', 'system-ui', 'sans-serif'],
                            'child': ['Nunito', 'Helvetica', 'sans-serif']
                        }

                    }

                }
            ]
          })
  ]
}