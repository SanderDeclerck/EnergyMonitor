import { faBolt, faBurn, faTint } from "@fortawesome/free-solid-svg-icons";

export const LOADING_ACTION = "LOADING_ACTION";
export const RESPONSE_COMPLETE = "RESPONSE_COMPLETE";
export const ERROR = "ERROR";

export function buildingDetailReducer(state, action) {
  if (action.type == LOADING_ACTION) {
    return { ...state, isLoading: true, isError: false, building: {} };
  }

  if (action.type == RESPONSE_COMPLETE) {
    return {
      ...state,
      isLoading: false,
      building: transformResponse(action.payload),
    };
  }

  if (action.type == ERROR) {
    return { ...state, isLoading: false, isError: true };
  }

  return state;
}

function transformResponse(response) {
  return { ...response, meters: response.meters.map(mapMeter) };
}

function mapMeter(meter) {
  return {
    ...meter,
    meterType: mapMeterType(meter.meterType),
    registers: meter.registers.map(mapRegister),
  };
}

function mapRegister(register) {
  return { ...register, tariff: mapRegisterTariff(register.tariff) };
}

function mapMeterType(meterType) {
  switch (meterType) {
    case 1:
      return { code: meterType, className: "electricity", icon: faBolt };
    case 2:
      return { code: meterType, className: "water", icon: faTint };
    case 3:
      return { code: meterType, className: "gas", icon: faBurn };
  }
}

function mapRegisterTariff(tariff) {
  switch (tariff) {
    case 1:
      return { code: tariff, name: "Peek" };
    case 2:
      return { code: tariff, name: "Off-peek" };
    case 3:
      return { code: tariff, name: "Day" };
  }
}
