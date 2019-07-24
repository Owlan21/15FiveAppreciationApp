import "reflect-metadata";
import { injectable } from 'inversify';
import IAppreciationAPIService from './Interfaces/IAppreciationAPIService';
import Appreciation from '@/models/Appreciation';

@injectable()
export default class AppreciationAPIService implements IAppreciationAPIService {
    private apiDomain: string = 'https://localhost:44343/api/appreciation';

    public returnHighFives(): Promise<Appreciation> {
        return fetch(
          `${this.apiDomain}`,
          {
            headers: {
              'Content-Type': 'application/json'
            },
            method: 'GET'
          }
        )
          .then(response => 
            response.json()
          .then(result => {
            return result;
          }))
          .catch(error => {
            throw error;
          });
      }
}
