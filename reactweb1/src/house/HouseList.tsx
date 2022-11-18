import { useState } from "react";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import ApiStatus from "../ApiStatus";
import config, { currencyFormatter } from "../config";
import useFetchHouses from "../hooks/HouseHook";
import { House } from "../types/house";

const HouseList = () => {
  const { data, status, isSuccess } = useFetchHouses();
  const nav = useNavigate();

  if (!isSuccess) return <ApiStatus status={status} />;

  return (
    <div>
      <div className="row mb-2">
        <h5 className="themeFontColor text-center">
          Houses currently on the market
        </h5>
      </div>
      <table className="table table-hover">
        <thead>
          <tr>
            <th>Address</th>
            <th>Country</th>
            <th>Asking Price</th>
          </tr>
        </thead>
        <tbody>
          {data &&
            data.map((h: House) => (
              <tr key={h.id} onClick={() => nav(`/house/${h.id}`)}>
                <td>{h.address}</td>
                <td>{h.country}</td>
                <td>{currencyFormatter.format(h.price)}</td>
              </tr>
            ))}
        </tbody>
      </table>

      <Link className="btn btn-primary" to="/house/add">
        Add
      </Link>
    </div>
  );
};

export default HouseList;
