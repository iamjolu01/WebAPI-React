import { useParams } from "react-router-dom";
import { useFetchHouse, useUpdateHouse } from "../hooks/HouseHooks";
import HouseForm from "./HouseForm";
import ApiStatus from "../ApiStatus";
import ValidationSummary from "../ValidationSummary";

const HouseEdit = () => {
  const { id } = useParams();
  if (!id) throw Error("Need a house id");
  const houseId = parseInt(id);

  const { data, status, isSuccess } = useFetchHouse(houseId);
  const updateHouseMutation = useUpdateHouse();

  if (!isSuccess) return <ApiStatus status={status} />;

  return (
    <>
      {updateHouseMutation.isError && (
        <ValidationSummary error={updateHouseMutation.error} />
      )}
      <HouseForm
        house={data}
        submitted={(house) => {
          updateHouseMutation.mutate(house);
        }}
      />
    </>
  );
};

export default HouseEdit;