import './App.css';
import SportsEsportsIcon from '@mui/icons-material/SportsEsports';
import TextField from '@mui/material/TextField';
import IconButton from '@mui/material/IconButton';
import SearchIcon from '@mui/icons-material/Search';
import { Box, Grid, Paper, Skeleton } from "@mui/material";
import axios from 'axios';
import { useState } from 'react';

function App() {

  const [genshinName, setGenshinName] = useState("");
  const [genshinInfo, setGenshinInfo] = useState<null | undefined | any>(undefined);
  const [genshinImage, setGenshinImage] = useState<null | undefined | any>(undefined);


  const genshin_BASE_API_URL = "https://api.genshin.dev";

  return (
    <div className="genshin-search-page" style={{ paddingTop: "1%", paddingBottom: "1%"}}>
      <div className="genshin-search-title">
        <h1 style={{ textAlign: "center"}}>
          Genshin Character Search  <SportsEsportsIcon />
        </h1>
      </div>
      <div className="genshin-search-field" style={{ display: "flex", justifyContent: "center", marginBottom: "1%" }}>
        <TextField
          id="search-bar"
          className="search"
          value={genshinName}
          onChange={(prop: any) => {
            setGenshinName(prop.target.value);
          }}
          label="Enter a Genshin Character name"
          variant="outlined"
          placeholder="Search..."
          size="medium"
        />
        <IconButton
          aria-label="search"
          onClick={() => {
            search();
          }}
          style={{ marginLeft: "1%", padding: "1%" }}
        >
          <SearchIcon style={{ fill: "black" }} />
        </IconButton>

      </div>
      {genshinInfo === undefined ? (
        <div></div>
      ) : (
        <div className="genshin-result" style={{ backgroundColor: getBackColor(genshinInfo), maxWidth: "600px", margin: "0 auto", border: "solid", padding: "2%", borderRadius: "80px", boxShadow: "5px 5px #888888"}}>
          <Paper sx={{ border: "dotted", borderRadius: "50px", paddingBottom: "1%"}}>
            <Grid container direction="row" spacing={5} sx={{ justifyContent: "center", paddingLeft: "1%" }}>
              <Grid item>
                <Box>
                  {genshinInfo === undefined || genshinInfo === null ? (
                    <h1 style={{textAlign: "center"}}>Genshin Character not found</h1>
                  ) : (
                    <div>
                      <h1 style={{textAlign: "center"}}>
                        {genshinInfo.name.charAt(0).toUpperCase() + genshinInfo.name.slice(1)}
                        <hr />
                      </h1>

                      <p style={{textAlign: "center"}}>
                        <b>Vision:</b> {genshinInfo.vision}
                        <br />
                        <b>Weapon:</b> {genshinInfo.weapon}
                        <br />
                        <b>Nation:</b> {genshinInfo.nation}
                        <br />
                        <b>Affiliation:</b> {genshinInfo.affiliation}
                        <br />
                        <b>Birthday: </b> {genshinInfo.birthday}
                        <br />
                        <b>Constellation:</b> {genshinInfo.constellation}
                      </p>
                      <hr />
                      <div>
                        <h2 style={{textAlign: "center"}}>Description</h2>
                        <Grid item>
                          <Box>
                            <p style={{textAlign: "center"}}>{genshinInfo.description}</p>
                          </Box>

                        </Grid>
                      </div>




                    </div>
                  )}

                </Box>
              </Grid>
              <Grid item>
                <Box>
                  {genshinInfo ? (
                    <img height="700px" width="400px" alt={genshinInfo.name} style={{ border: "solid", borderRadius: "10px", borderColor: getVisionColor(genshinInfo), boxShadow: "5px 5px #888888"}}
                      src={genshinImage}></img>
                  ) : (
                    <Skeleton width={300} height={300} />
                  )
                  }

                </Box>
              </Grid>
            </Grid>
          </Paper>
        </div>
      )}

    </div>




  );

  function getVisionColor(visions: { vision: any; } | null | undefined) {
    let backColor = "#000000";
    if (visions === undefined || visions === null) {
      return backColor;
    }
    const vision = visions.vision;
    if (vision.includes("Anemo")) {
      backColor = "#54DCB4";
    }
    else if (vision.includes("Geo")){
      backColor = "#D3A94C"
    }
    else if (vision.includes("Electro")){
      backColor = "#442F5D"
    }
    else if (vision.includes("Pyro")){
      backColor = "#A43324"
    }
    else if (vision.includes("Cryo")){
      backColor = "#486081"
    }
    else if (vision.includes("Hydro")){
      backColor = "#03A9D9"
    }

    
    
    return backColor;
  }

  function getBackColor(genshin: { nation: any; } | null | undefined) {
    let backColor = "#FFFFFF";
    if (genshin === undefined || genshin === null) {
      return backColor;
    }
    const nation = genshin.nation;
    if (nation.includes("Mondstadt")) {
      backColor = "#54DCB4";
    }
    else if (nation.includes("Liyue")){
      backColor = "#D3A94C"
    }
    else if (nation.includes("Inazuma")){
      backColor = "#442F5D"
    }
    
    return backColor;
  }



  function search() {
    console.log(genshinName);
    if (genshinName === undefined || genshinName === "") {
      return;
    }
    axios.get(genshin_BASE_API_URL + "/characters/" + genshinName)
      .then((res) => {
        setGenshinInfo(res.data);
        setGenshinImage(genshin_BASE_API_URL + "/characters/" + genshinName + "/card.png");
        console.log(res.data);

      })
      .catch(() => {
        setGenshinInfo(null);
      });

  }


}
export default App;
