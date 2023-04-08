import { ThunkAction } from 'redux-thunk';

import { RootState } from '../store/store';
import { Action as CustomerActions } from '../actionTypes/customerActionTypes';
import { ICustomer as Customer } from '../../models/customer';

export type RootActions = 
    | CustomerActions;

export type ThunkResult<R> = ThunkAction<R, RootState, undefined, RootActions>;

export interface MultiRecordResponse
{
    customers: Customer[],
    success: boolean,
    message: string,
    errors: string[]
}
