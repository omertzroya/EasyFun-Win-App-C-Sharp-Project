
# NBA Scores Web App
This is a web application built with Node.js and Express that fetches and displays real-time NBA scores using the RapidAPI service. The scores are dynamically updated, showcasing the most recent games played in the last month.

## Main Features

1. **Responsive Design:**
   - The web app is built with a responsive design, allowing users to access and enjoy the NBA scores from any device comfortably.

2. **API Integration:**
   - Utilizes the RapidAPI service to access NBA scores, leveraging a reliable source for up-to-date sports information.

## Setup

1. **Clone the Repository:**
   ```
   git clone https://github.com/AsafTzabari/NBA-Scores-FullStack-Project.git
   ```

2. **Install Dependencies:**
   ```
   cd //Your current path 
   npm install
   ```

3. **Run the Application:**
   ```
   node index.js
   ```

   The server will start on [http://localhost:3000](http://localhost:3000).

## API Key Setup

To successfully run the application, you need to obtain a RapidAPI key as it is used to access the NBA scores API. Follow these steps:

1. Visit [RapidAPI](https://rapidapi.com/) and create an account.
2. Subscribe to the [NBA Odds API](https://rapidapi.com/theoddsapi/api/live-sports-odds) to get your API key.
3. Replace the placeholder `'YOUR-RAPIDAPI-KEY'` in the `X-RapidAPI-Key` header with your actual API key in the `app.get` route in `app.js`.

## Technology Used
<div>
  <img src='https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white' alt='JQUERY'/>
  <img src='https://img.shields.io/badge/JavaScript-323330?style=for-the-badge&logo=javascript&logoColor=F7DF1E' alt='JavaScript'/>
  <img src='https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white' alt='CSS3'/>
  <img src='https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white' alt='HTML5'/>
  <img src='https://img.shields.io/badge/Node.js-43853D?style=for-the-badge&logo=node.js&logoColor=white' alt='Node.js'/>
  <img src='https://img.shields.io/badge/Express-000000?style=for-the-badge&logo=express&logoColor=white' alt='Express'/>
  <img src='https://img.shields.io/badge/Axios-35495E?style=for-the-badge&logo=axios&logoColor=white' alt='Axios'/>
</div>



## Dependencies
- Express: Web framework for Node.js
- Axios: HTTP client for making API requests
- EJS: Embedded JavaScript templating for rendering dynamic content

## Screenshots
<div style="display: flex; justify-content: space-between;">
    <div style="flex: 1; text-align: center;">
        <img src="public/images/Screenshots1.png" width="300" alt="Screenshot 1">
       <img src="public/images/Screenshots2.png" width="300" alt="Screenshot 2">
       
</div>



## Contributing

Feel free to contribute to the project by opening issues or creating pull requests. 

## License

![GitHub](https://img.shields.io/github/license/ItsAlexanderPopov/Simon-game)


---

Enjoy exploring NBA scores with this web app! If you have any issues or suggestions, please open an issue in the GitHub repository.















