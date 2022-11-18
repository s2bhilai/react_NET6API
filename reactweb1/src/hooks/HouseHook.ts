import { AxiosError, AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import {
  QueryClient,
  useMutation,
  useQuery,
  useQueryClient,
} from "react-query";
import config from "../config";
import { House } from "../types/house";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import Problem from "../types/problem";

const useFetchHouses = () => {
  return useQuery<House[], AxiosError>("houses", () => {
    return axios.get(`${config.baseApiUrl}/houses`).then((resp) => resp.data);
  });
};

const useFetchHouse = (id: number) => {
  return useQuery<House, AxiosError>(["houses", id], () => {
    return axios
      .get(`${config.baseApiUrl}/houses/${id}`)
      .then((resp) => resp.data);
  });
};

const useAddHouse = () => {
  const nav = useNavigate();
  const queryClient = useQueryClient();

  return useMutation<AxiosResponse, AxiosError<Problem>, House>(
    (h) => axios.post(`${config.baseApiUrl}/houses`, h),
    {
      onSuccess: () => {
        queryClient.invalidateQueries("houses");
        nav("/");
      },
    }
  );
};

const useUpdateHouse = () => {
  const nav = useNavigate();
  const queryClient = useQueryClient();

  return useMutation<AxiosResponse, AxiosError<Problem>, House>(
    (h) => axios.put(`${config.baseApiUrl}/houses`, h),
    {
      onSuccess: (_, house) => {
        queryClient.invalidateQueries("houses");
        nav(`/house/${house.id}`);
      },
    }
  );
};

const useDeleteHouse = () => {
  const nav = useNavigate();
  const queryClient = useQueryClient();

  return useMutation<AxiosResponse, AxiosError, House>(
    (h) => axios.delete(`${config.baseApiUrl}/houses/${h.id}`),
    {
      onSuccess: () => {
        queryClient.invalidateQueries("houses");
        nav("/");
      },
    }
  );
};

export default useFetchHouses;

export { useFetchHouse, useAddHouse, useUpdateHouse, useDeleteHouse };

//UseEffect
//- Load only once, so data kind of static
//- When the custom hook is used in multiple components it will fetch the data each time
// - If hook there in 10 components, total 10 times hook will be called.
// - We dont have retry mechanism.
// - We need shared state, caching, controlled re-fetches and retries.
// - Enter the screen - REACT-QUERY (claps all around)

//- Cached data will be marked as stale right after fetch.
//- Re-fetch will only occur when new mount of query occurs.
//- Or the browser refocuses.
//- or when the network is reconnected.
//- If needed, deviate ffrom defaults.
