import { Component, OnInit, inject } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'] // ✅ fixed here
})
export class AppComponent implements OnInit {
  private router = inject(Router);
  private auth = inject(AuthService);
  title = 'LuftBornTask-FE';

  ngOnInit(): void {
    this.auth.appState$.subscribe((state) => {
      if (state?.target) {
        this.router.navigateByUrl(state.target);
      }
    });
  }
}
