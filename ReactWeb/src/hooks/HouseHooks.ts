import { House } from "../types/house";
import config from "../types/config";
import { useQuery } from "@tanstack/react-query";
import axios, { AxiosError } from "axios";

const useFetchHouses = () => {
    return useQuery<House[], AxiosError>({
        queryKey: ["houses"],
        queryFn: () =>
            axios.get(`${config.baseApiUrl}/houses`).then((resp) => resp.data),
    });
};

const useFetchHouse = (id: number) => {
    return useQuery<House, AxiosError>({
        queryKey: ["house", id],
        queryFn: () =>
            axios.get(`${config.baseApiUrl}/houses/${id}`).then((resp) => resp.data),
    });
};

export default useFetchHouses;
export { useFetchHouse }