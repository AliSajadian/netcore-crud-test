import { combineReducers } from 'redux';

import { CustomerReducer, ICustomersState as CustomersState } from './customerReducer';
    
interface RootStateType {
    readonly customers: CustomersState;
}

const rootReducer = combineReducers<RootStateType>({
    customers: CustomerReducer
});

export default rootReducer;