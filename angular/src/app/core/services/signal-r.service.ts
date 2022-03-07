import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class SignalRService {
    private hubConnection: signalR.HubConnection;

    private event = new BehaviorSubject<any>(null);
    castKanban = this.event.asObservable();

    editKanban(newUser) {
        this.event.next(newUser);
    }
}
