import React from "react";
import { Card, CardBody, Input, Button } from "reactstrap";
import { useState, useEffect } from "react";

export const SearchBar = ({ setVideos }) => {
  const [search, updateSearch] = useState({
    sTerm: "",
    sort: false,
  });

  const handleInputChange = (event) => {
    updateSearch({ sTerm: event.target.value, sort: search.sort });
  };

  const handleSortChange = (event) => {
    updateSearch({
      sTerm: search.sTerm,
      sort: !search.sort,
    });
  };

  const baseUrl = (term, sort) => {
    return `/api/video/search?q=${term}&sortDesc=${sort}`;
  };

  const searchNow = () => {
    fetch(baseUrl(search.sTerm, search.sort))
      .then((res) => res.json())
      .then((res) => setVideos(res));
  };

  return (
    <div>
      <Input id="search" onChange={handleInputChange} />
      <Button id="sort" onClick={handleSortChange}>
        Sort?{" "}
      </Button>
      <Button onClick={searchNow}>Search</Button>
    </div>
  );
};
