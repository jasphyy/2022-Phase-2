import { useState } from 'react';
import { Pokemon } from 'pokenode-ts';
import IconButton from '@mui/material/IconButton';
import SearchIcon from '@mui/icons-material/Search';
import TextField from '@mui/material/TextField';
import axios from 'axios';
import './App.css';

function App() {

  const [pokemonName, setPokemonName] = useState("");
  const [pokemonInfo, setPokemonInfo] = useState<undefined | Pokemon>(undefined);

  const POKEMON_BASE_URL = "https://pokeapi.co/api/v2";

  return (
    <div>
      <h1>
        Pokemon Search
      </h1>
      <div>
        <TextField
          id="search-bar"
          className="text"
          value={pokemonName}
          onChange={(prop: any) => {
            setPokemonName(prop.target.value);
          }}
          label="Enter a Pokémon Name..."
          variant="outlined"
          placeholder="Search..."
          size="small"
        />
        <IconButton
          aria-label="search"
          onClick={() => {
            search();
          }}
        >
          <SearchIcon style={{ fill: "blue" }} />

        </IconButton>
      </div>

      <p>
        You have entered {pokemonName}
      </p>

      {pokemonInfo === undefined ? (
        <p>Pokemon not found</p>
      ) : (
        <div id="pokemon-result">
          {pokemonInfo.sprites.other.dream_world.front_default === null ? (
            <p>No image found</p>
          ) : (
            <img src={pokemonInfo.sprites.other.dream_world.front_default} />
          )}
          <p>
            Height: {pokemonInfo.height * 10} cm
            <br />
            Weight: {pokemonInfo.weight / 10} Kilograms
          </p>

        </div>
      )}

    </div>
  );

  function search() {
    axios.get(POKEMON_BASE_URL + "/pokemon/" + pokemonName.toLowerCase()).then((res) => {
      setPokemonInfo(res.data);
    })
      .catch((err) => {
        console.log("Pokemon not found");
        setPokemonInfo(undefined);
      });






  }
}

export default App;
