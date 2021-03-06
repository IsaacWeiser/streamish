const baseUrl = "/api/video/GetWithComments";

export const getAllVideos = () => {
  return fetch(baseUrl).then((res) => res.json());
};

export const getVideo = (id) => {
  return fetch(`${baseUrl}/${id}`).then((res) => res.json());
};

export const addVideo = (video) => {
  return fetch(baseUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(video),
  });
};
