import { Component, OnInit } from '@angular/core';
import {FeaturedPost} from '../../../../Models/FeaturedPost';
import {StoryPost} from '../../../../Models/StoryPost';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  FeaturedPostData: FeaturedPost[];
  StoryPostData: StoryPost[];
  constructor() {
    this.FeaturedPostData = [
      {
        FeaturedPostThumbnail: 'assets/img/demopic/1.jpg',
        Title: 'We\'re living some strange times',
        Text: 'This is a longer card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.',
        AuthorThumbnail: 'https://www.gravatar.com/avatar/e56154546cf4be74e393c62d1ae9f9d4?s=250&amp;d=mm&amp;r=x',
        AuthorName: 'Steve',
        PostedDate: '22 July 2017',
        ReadTimeSec: '6 min read'
      },
      {
        FeaturedPostThumbnail: 'assets/img/demopic/2.jpg',
        Title: 'The beauty of this world is in your heart',
        Text: 'This is a longer card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.',
        AuthorThumbnail: 'https://www.gravatar.com/avatar/e56154546cf4be74e393c62d1ae9f9d4?s=250&amp;d=mm&amp;r=x',
        AuthorName: 'Jane',
        PostedDate: '22 July 2017',
        ReadTimeSec: '6 min read'
      },
      {
        FeaturedPostThumbnail: 'assets/img/demopic/3.jpg',
        Title: 'Dreaming of Las Vegas Crazyness',
        Text: 'This is a longer card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.',
        AuthorThumbnail: 'https://www.gravatar.com/avatar/e56154546cf4be74e393c62d1ae9f9d4?s=250&amp;d=mm&amp;r=x',
        AuthorName: 'Mary',
        PostedDate: '22 July 2017',
        ReadTimeSec: '6 min read'
      },
      {
        FeaturedPostThumbnail: 'assets/img/demopic/4.jpg',
        Title: 'San Francisco at its best view in all seasons',
        Text: 'This is a longer card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.',
        AuthorThumbnail: 'https://www.gravatar.com/avatar/e56154546cf4be74e393c62d1ae9f9d4?s=250&amp;d=mm&amp;r=x',
        AuthorName: 'Sal',
        PostedDate: '22 July 2017',
        ReadTimeSec: '6 min read'
      }
    ];
    this.StoryPostData = [
      {
        StoryPostThumbnail: 'assets/img/demopic/5.jpg',
        Title: 'Autumn doesn\'t have to be nostalgic, you know?',
        Text: 'This is a longer card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.',
        AuthorThumbnail: 'https://www.gravatar.com/avatar/e56154546cf4be74e393c62d1ae9f9d4?s=250&amp;d=mm&amp;r=x',
        AuthorName: 'Sal',
        PostedDate: '22 July 2017',
        ReadTimeSec: '6 min read',
        postId: 1
      },
      {
        StoryPostThumbnail: 'assets/img/demopic/6.jpg',
        Title: 'Best galleries in the world with photos',
        Text: 'This is a longer card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.',
        AuthorThumbnail: 'https://www.gravatar.com/avatar/e56154546cf4be74e393c62d1ae9f9d4?s=250&amp;d=mm&amp;r=x',
        AuthorName: 'Sal',
        PostedDate: '22 July 2017',
        ReadTimeSec: '6 min read',
        postId: 1
      },
      {
        StoryPostThumbnail: 'assets/img/demopic/7.jpg',
        Title: 'Little red dress and a perfect summer',
        Text: 'This is a longer card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.',
        AuthorThumbnail: 'https://www.gravatar.com/avatar/e56154546cf4be74e393c62d1ae9f9d4?s=250&amp;d=mm&amp;r=x',
        AuthorName: 'Sal',
        PostedDate: '22 July 2017',
        ReadTimeSec: '6 min read',
        postId: 1
      },
      {
        StoryPostThumbnail: 'assets/img/demopic/8.jpg',
        Title: 'Thinking outside the box can help you prosper',
        Text: 'This is a longer card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.',
        AuthorThumbnail: 'https://www.gravatar.com/avatar/e56154546cf4be74e393c62d1ae9f9d4?s=250&amp;d=mm&amp;r=x',
        AuthorName: 'Sal',
        PostedDate: '22 July 2017',
        ReadTimeSec: '6 min read',
        postId: 1
      },
      {
        StoryPostThumbnail: 'assets/img/demopic/9.jpg',
        Title: '10 Things you should know about choosing your house',
        Text: 'This is a longer card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.',
        AuthorThumbnail: 'https://www.gravatar.com/avatar/e56154546cf4be74e393c62d1ae9f9d4?s=250&amp;d=mm&amp;r=x',
        AuthorName: 'Sal',
        PostedDate: '22 July 2017',
        ReadTimeSec: '6 min read',
        postId: 1
      },
      {
        StoryPostThumbnail: 'assets/img/demopic/10.jpg',
        Title: 'Visiting the world means learning cultures',
        Text: 'This is a longer card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.',
        AuthorThumbnail: 'https://www.gravatar.com/avatar/e56154546cf4be74e393c62d1ae9f9d4?s=250&amp;d=mm&amp;r=x',
        AuthorName: 'Sal',
        PostedDate: '22 July 2017',
        ReadTimeSec: '6 min read',
        postId: 1
      },
    ];
  }

  ngOnInit(): void {
  }

}
